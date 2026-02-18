using MediaRatingsPlatform.DataAccessLayer.Interfaces;

namespace MediaRatingsPlatform.DataAccessLayer.PostgreSqlRepository;

public class PostgreSqlRepositoryFactory : IRepositoryFactory {
    private readonly string _connectionString;

    public PostgreSqlRepositoryFactory(string connectionString) {
        _connectionString = connectionString;
    }

    public IUserRepository CreateUserRepository() {
        return new PostgreSqlUserRepository(_connectionString);
    }

    public ITokenRepository CreateTokenRepository() {
        return new PostgreSqlTokenRepository(_connectionString);
    }

    public IRatingRepository CreateRatingRepository() {
        return new PostgreSqlRatingRepository(_connectionString);
    }

    public IMediaRepository CreateMediaRepository() {
        return new PostgreSqlMediaRepository(_connectionString);
    }
}
