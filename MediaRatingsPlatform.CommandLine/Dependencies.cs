using MediaRatingsPlatform.BusinessLayer;
using MediaRatingsPlatform.BusinessLayer.Interfaces;
using MediaRatingsPlatform.DataAccessLayer.Interfaces;
using MediaRatingsPlatform.DataAccessLayer.PostgreSqlRepository;
using MediaRatingsPlatform.PresentationLayer;

namespace MediaRatingsPlatform;

// This file sets all dependencies which are then injected into classes later on
public class Dependencies(string dbConnectionString) : IDependencies {
    private readonly IRepositoryFactory _database = new PostgreSqlRepositoryFactory(dbConnectionString);

    public IRepositoryFactory GetDatabase() {
        return _database;
    }

    public IAuthenticator GetAuth() {
        return new Authenticator(_database);
    }

    public IUserManager GetUserManager() {
        return new UserManager(_database);
    }

    public IFavouriteManager GetFavouriteManager() {
        return new FavouriteManager(_database);
    }

    public IMediaManager GetMediaManager() {
        return new MediaManager(_database);
    }

    public IRatingManager GetRatingManager() {
        return new RatingManager(_database);
    }

    public IStatisticsManager GetStatisticsManager() {
        return new StatisticsManager(_database);
    }

    public IRecommendationManager GetRecommendationManager() {
        return new RecommendationManager(_database);
    }
}
