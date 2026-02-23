using MediaRatingsPlatform.BusinessLayer.Interfaces;
using MediaRatingsPlatform.DataAccessLayer.Interfaces;
using MediaRatingsPlatform.SharedObjects;

namespace MediaRatingsPlatform.BusinessLayer;

public class MediaManager(IRepositoryFactory database) : IMediaManager {
    public IEnumerable<Media> ListMedias(Media filter) {
        throw new ApiNotImplementedException();
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
