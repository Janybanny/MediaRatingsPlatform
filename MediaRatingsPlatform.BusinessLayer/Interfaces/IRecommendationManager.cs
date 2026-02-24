using MediaRatingsPlatform.SharedObjects;

namespace MediaRatingsPlatform.BusinessLayer.Interfaces;

public interface IRecommendationManager {
    public List<Media> GetRecommendationsByGenre(User user, IMediaManager mediaManager);
    public List<Media> GetRecommendationsByContent(User user, IMediaManager mediaManager);
}
