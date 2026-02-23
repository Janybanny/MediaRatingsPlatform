using MediaRatingsPlatform.BusinessLayer.Interfaces;
using MediaRatingsPlatform.DataAccessLayer.Interfaces;

namespace MediaRatingsPlatform.PresentationLayer;

public interface IDependencies {
    public IRepositoryFactory GetDatabase();
    public IAuthenticator GetAuth();
    public IUserManager GetUserManager();
    public IFavouriteManager GetFavouriteManager();
    public IMediaManager GetMediaManager();
    public IRatingManager GetRatingManager();
    public IStatisticsManager GetStatisticsManager();
}
