using Npgsql;

namespace MediaRatingsPlatform.DataAccessLayer.PostgreSqlRepository;

public abstract class PostgreSqlBaseRepository(string connectionString) {
    private readonly string _connectionString = connectionString;

    protected T ExecuteWithDbConnection<T>(Func<NpgsqlConnection, T> command) {
        try {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            return command(connection);
        } catch (NpgsqlException e) {
            throw new DataAccessFailedException("Could not connect to database", e);
        }
    }
}
