using MediaRatingsPlatform.BusinessLayer.Interfaces;
using MediaRatingsPlatform.DataAccessLayer.Interfaces;
using MediaRatingsPlatform.SharedObjects;

namespace MediaRatingsPlatform.BusinessLayer;

public class RecommendationManager(IRepositoryFactory database) : IRecommendationManager {
    public List<Media> GetRecommendationsByGenre(User user) {
        throw new ApiNotImplementedException();
    }

    public List<Media> GetRecommendationsByContent(User user) {
        throw new ApiNotImplementedException();
    }
}
