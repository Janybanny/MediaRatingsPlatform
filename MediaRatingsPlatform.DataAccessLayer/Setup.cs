using Npgsql;

namespace MediaRatingsPlatform.DataAccessLayer;

public class Setup
{
    /*
    private const string CreateTablesCommand = """
        CREATE TABLE IF NOT EXISTS users (username varchar PRIMARY KEY, password varchar, token varchar);
        CREATE TABLE IF NOT EXISTS messages (id serial PRIMARY KEY, text varchar, username varchar REFERENCES users ON DELETE CASCADE);

        id bigint GENERATED ALWAYS AS IDENTITY
        CREATE TABLE IF NOT EXISTS
        ON DELETE CASCADE);

        CREATE TABLE IF NOT EXISTS users (username varchar PRIMARY KEY, password varchar, token varchar);
        CREATE TABLE IF NOT EXISTS messages (id serial PRIMARY KEY, text varchar, username varchar REFERENCES users ON DELETE CASCADE);
        """;

    private readonly string _connectionString;

    public Setup(string connectionString) {
        using var connection = new NpgsqlConnection(connectionString);
        connection.Open();
        using var cmd = new NpgsqlCommand(CreateTablesCommand, connection);
        cmd.ExecuteNonQuery();
    }

    public IUserRepository CreateUserRepository()
    {
        return new PostgreSqlUserRepository(_connectionString);
    }

    public IMessageRepository CreateMessageRepository()
    {
        return new PostgreSqlMessageRepository(_connectionString);
    }

    private void EnsureTables(string connectionString)
    {
        using var connection = new NpgsqlConnection(connectionString);
        connection.Open();
        using var cmd = new NpgsqlCommand(CreateTablesCommand, connection);
        cmd.ExecuteNonQuery();
    }
    */
}
