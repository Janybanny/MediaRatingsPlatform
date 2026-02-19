using MediaRatingsPlatform.SharedObjects;
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
            Console.WriteLine($"DBError: {e.Message}");
            throw new ApiDatabaseException();
        }
    }
}
