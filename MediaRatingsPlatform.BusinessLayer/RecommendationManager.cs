using MediaRatingsPlatform.BusinessLayer.Interfaces;
using MediaRatingsPlatform.DataAccessLayer.Interfaces;
using MediaRatingsPlatform.SharedObjects;

namespace MediaRatingsPlatform.BusinessLayer;

public class RecommendationManager(IRepositoryFactory database) : IRecommendationManager {
    public List<Media> GetRecommendationsByGenre(User user, IMediaManager mediaManager) {
        var ratings = database.CreateRatingRepository().GetRatingsByUser(user);
        var weightedMediaIds = createGenreIndex(user, ratings)
            .OrderByDescending(kvp => kvp.Value)
            .Select(kvp => kvp.Key)
            .ToList();
        List<Media> recommendations = [];
        foreach (var mediaId in weightedMediaIds) recommendations.Add(mediaManager.GetMedia(new Media { Id = mediaId }, (int)user.Id!)!);
        return recommendations;
    }

    public List<Media> GetRecommendationsByContent(User user, IMediaManager mediaManager) {
        var ratings = database.CreateRatingRepository().GetRatingsByUser(user);
        var mergedWeightedMediaIds = createGenreIndex(user, ratings)
            .Concat(createMediaTypeIndex(user, ratings))
            .Concat(createAgeRatingIndex(user, ratings))
            .GroupBy(kvp => kvp.Key)
            .ToDictionary(group => group.Key, group => group.Sum(kvp => kvp.Value))
            .OrderByDescending(kvp => kvp.Value)
            .Select(kvp => kvp.Key)
            .ToList();
        List<Media> recommendations = [];
        foreach (var mediaId in mergedWeightedMediaIds) recommendations.Add(mediaManager.GetMedia(new Media { Id = mediaId }, (int)user.Id!)!);
        return recommendations;
    }

    private Dictionary<int, int> createGenreIndex(User user, List<Rating> ratings) {
        var genredb = database.CreateGenreRepository();
        var genreWeightings = new Dictionary<string, int>();
        foreach (var rating in ratings)
        foreach (var genre in genredb.GetGenres(new Genre { MediaId = rating.MediaId })) {
            genreWeightings.TryGetValue(genre.Name!, out var currentWeighting);
            genreWeightings[genre.Name!] = (int)(currentWeighting + rating.Stars)!;
        }
        var genres = genredb.GetAllGenreEntries();
        var mediaWeightings = new Dictionary<int, int>();
        foreach (var genre in genres) {
            genreWeightings.TryGetValue(genre.Name!, out var weighting);
            mediaWeightings.TryGetValue((int)genre.MediaId!, out var currentWeighting);
            mediaWeightings[(int)genre.MediaId!] = currentWeighting + weighting;
        }
        return mediaWeightings;
    }

    private Dictionary<int, int> createMediaTypeIndex(User user, List<Rating> ratings) {
        var mediadb = database.CreateMediaRepository();
        var mediaTypeWeightings = new Dictionary<string, int>();
        var mediaTypeList = new Dictionary<int, string>();
        foreach (var rating in ratings) {
            var media = mediadb.GetMedia(new Media { Id = rating.MediaId });
            mediaTypeList[(int)media.Id!] = media.MediaType;
            mediaTypeWeightings.TryGetValue(media!.MediaType!, out var currentWeighting);
            mediaTypeWeightings[media!.MediaType!] = (int)(currentWeighting + rating.Stars)!;
        }
        var mediaWeightings = new Dictionary<int, int>();
        foreach (var media in mediaTypeList) {
            mediaTypeWeightings.TryGetValue(media.Value, out var weighting);
            mediaWeightings[media.Key] = weighting;
        }
        return mediaWeightings;
    }

    private Dictionary<int, int> createAgeRatingIndex(User user, List<Rating> ratings) {
        var mediadb = database.CreateMediaRepository();
        var mediaTypeWeightings = new Dictionary<int, int>();
        var mediaTypeList = new Dictionary<int, int>();
        foreach (var rating in ratings) {
            var media = mediadb.GetMedia(new Media { Id = rating.MediaId });
            mediaTypeList[(int)media!.Id!] = (int)media.AgeRestriction!;
            mediaTypeWeightings.TryGetValue((int)media!.AgeRestriction!, out var currentWeighting);
            mediaTypeWeightings[(int)media!.AgeRestriction!] = (int)(currentWeighting + rating.Stars)!;
        }
        var mediaWeightings = new Dictionary<int, int>();
        foreach (var media in mediaTypeList) {
            mediaTypeWeightings.TryGetValue(media.Value!, out var weighting);
            mediaWeightings[media.Key] = weighting;
        }
        return mediaWeightings;
    }
}
