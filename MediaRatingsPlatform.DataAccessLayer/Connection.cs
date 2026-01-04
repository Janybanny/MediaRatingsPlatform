using Npgsql;

namespace MediaRatingsPlatform.DataAccessLayer;

public class Connection
{
    private readonly string _connectionString;
    private NpgsqlConnection activeConnection;
    public Connection(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void Setup()
    {
        activeConnection = new NpgsqlConnection(_connectionString);
    }
    /*
try {
    using var connection = new NpgsqlConnection(_connectionString);
    connection.Open();

    using var cmd = new NpgsqlCommand(SelectUserByTokenCommand, connection);
    cmd.Parameters.AddWithValue("token", authToken);
    User? user = null;
    // take the first row, if any
    using var reader = cmd.ExecuteReader();
    if (reader.Read())
    {
        user = ReadUser(reader);
    }
    return user;
} catch (NpgsqlException e) {
    throw new DataAccessFailedException("Could not connect to database", e);
}
*/
}