using MediaRatingsPlatform.BusinessLayer.Interfaces;
using MediaRatingsPlatform.DataAccessLayer.Interfaces;
using MediaRatingsPlatform.SharedObjects;

namespace MediaRatingsPlatform.BusinessLayer;

public class MediaManager(IRepositoryFactory database) : IMediaManager {
    public IEnumerable<Media> ListMedias(MediaFilter filter, int userId) {
        IEnumerable<int> mediaIds = database.CreateMediaRepository().GetAllMediaIds();
        List<Media> fullmedias = [];
        foreach (var mediaId in mediaIds) fullmedias.Add(GetMedia(new Media { Id = mediaId }, userId));
        var medias = fullmedias.AsEnumerable();
        // filter with linq
        if (!string.IsNullOrEmpty(filter.Title)) medias = medias.Where(m => m.Title != null && m.Title.Contains(filter.Title, StringComparison.OrdinalIgnoreCase));
        if (!string.IsNullOrEmpty(filter.Genre)) medias = medias.Where(m => m.Genres != null && m.Genres.Contains(filter.Genre, StringComparer.OrdinalIgnoreCase));
        if (!string.IsNullOrEmpty(filter.MediaType)) medias = medias.Where(m => m.MediaType != null && m.MediaType.Equals(filter.MediaType, StringComparison.OrdinalIgnoreCase));
        if (filter.ReleaseYear.HasValue) medias = medias.Where(m => m.ReleaseYear == filter.ReleaseYear.Value);
        if (filter.AgeRestriction.HasValue) medias = medias.Where(m => m.AgeRestriction == null || m.AgeRestriction <= filter.AgeRestriction.Value);
        if (filter.Rating.HasValue) medias = medias.Where(m => m.AverageRating >= filter.Rating.Value);
        switch (filter.SortBy?.ToLower()) {
            case "year":
                medias = medias.OrderBy(m => m.ReleaseYear);
                break;
            case "score":
                medias = medias.OrderByDescending(m => m.AverageRating);
                break;
            case "title":
            default:
                medias = medias.OrderBy(m => m.Title);
                break;
        }
        return medias.ToList();
    }

    public void AddMedia(Media media) {
        media.Id = database.CreateMediaRepository().AddMedia(media);
        var genredb = database.CreateGenreRepository();
        foreach (var genre in media.Genres!) genredb.AddGenre(new Genre { MediaId = media.Id, Name = genre });
    }

    public void UpdateMedia(Media media) {
        database.CreateMediaRepository().UpdateMedia(media);
        var genredb = database.CreateGenreRepository();
        var existingGenres = genredb.GetGenres(new Genre { MediaId = media.Id });
        foreach (var genre in existingGenres)
            if (!media.Genres!.Contains(genre.Name))
                genredb.RemoveGenre(genre);
        foreach (var genre in media.Genres!)
            if (!existingGenres.Contains(new Genre { MediaId = media.Id, Name = genre }))
                genredb.AddGenre(new Genre { MediaId = media.Id, Name = genre });
    }

    public void DeleteMedia(Media media) {
        database.CreateMediaRepository().DeleteMedia(media);
    }

    public Media GetMedia(Media media, int userId) {
        media = database.CreateMediaRepository().GetMedia(media)!;
        if (media == null) throw new ApiItemDoesNotExistException();
        var genres = database.CreateGenreRepository().GetGenres(new Genre { MediaId = media.Id });
        media.Genres = [];
        foreach (var genre in genres) media.Genres.Add(genre.Name!);
        media.Ratings = database.CreateRatingRepository().GetRatingsByMedia(media);
        if (media.Ratings.Count != 0) {
            var counter = 0;
            var stars = 0;
            foreach (var rating in media.Ratings) {
                if (!(bool)rating.CommentVisible! && rating.Owner != userId) rating.Comment = null;
                counter++;
                stars += (int)rating.Stars!;
            }
            media.AverageRating = stars / counter!;
        }
        return media;
    }
}
