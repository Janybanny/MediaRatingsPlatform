using MediaRatingsPlatform.SharedObjects;

namespace MediaRatingsPlatform.BusinessLayer.Interfaces;

public interface IRecommendationManager {
    public List<Media> GetRecommendationsByGenre(User user);
    public List<Media> GetRecommendationsByContent(User user);
}
