using MediaRatingsPlatform.DataAccessLayer.Interfaces;

namespace MediaRatingsPlatform.DataAccessLayer.PostgreSqlRepository;

public class PostgreSqlRepositoryFactory(string connectionString) : IRepositoryFactory {
    public IUserRepository CreateUserRepository() {
        return new PostgreSqlUserRepository(connectionString);
    }

    public ITokenRepository CreateTokenRepository() {
        return new PostgreSqlTokenRepository(connectionString);
    }

    public IRatingRepository CreateRatingRepository() {
        return new PostgreSqlRatingRepository(connectionString);
    }

    public IMediaRepository CreateMediaRepository() {
        return new PostgreSqlMediaRepository(connectionString);
    }

    public IFavouriteRepository CreateFavouriteRepository() {
        return new PostgreSqlFavouriteRepository(connectionString);
    }
}
