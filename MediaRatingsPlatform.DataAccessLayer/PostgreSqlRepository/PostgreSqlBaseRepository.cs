using Npgsql;

namespace MediaRatingsPlatform.DataAccessLayer.PostgreSqlRepository;

public class PostgreSqlBaseRepository {
    protected static T ExecuteWithDbConnection<T>(Func<NpgsqlConnection, T> command) {
        try {
            using var connection = new NpgsqlConnection(DatabaseCredentials.ConnectionString);
            connection.Open();
            return command(connection);
        } catch (NpgsqlException e) {
            throw new DataAccessFailedException("Could not connect to database", e);
        }
    }
}
