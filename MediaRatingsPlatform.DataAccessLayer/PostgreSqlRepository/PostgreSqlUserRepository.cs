using System.Data;
using MediaRatingsPlatform.DataAccessLayer.Interfaces;
using MediaRatingsPlatform.SharedObjects;
using Npgsql;

namespace MediaRatingsPlatform.DataAccessLayer.PostgreSqlRepository;

public class PostgreSqlUserRepository(string connectionString) : PostgreSqlBaseRepository(connectionString), IUserRepository {
    public User? GetUser(User input) {
        return ExecuteWithDbConnection(connection => {
            IDataRecord? record = null;
            // uses Id, falls back to Username if no Id provided
            if (input.Id == null) {
                using var cmd = new NpgsqlCommand("SELECT * FROM users WHERE username=@username", connection);
                cmd.Parameters.AddWithValue("username", input.Username!);
                using var reader = cmd.ExecuteReader();
                if (reader.Read()) record = reader;
                return record == null
                    ? null
                    : new User {
                        Id = Convert.ToInt32(record["userid"]), Username = Convert.ToString(record["username"]), Password = Convert.ToString(record["password"]), EMail = Convert.ToString(record["email"]), FavoriteGenre = Convert.ToString(record["favouritegenre"])
                    };
            } else {
                using var cmd = new NpgsqlCommand("SELECT * FROM users WHERE userid=@userid", connection);
                cmd.Parameters.AddWithValue("userid", input.Id!);
                using var reader = cmd.ExecuteReader();
                if (reader.Read()) record = reader;
                return record == null
                    ? null
                    : new User {
                        Id = Convert.ToInt32(record["userid"]), Username = Convert.ToString(record["username"]), Password = Convert.ToString(record["password"]), EMail = Convert.ToString(record["email"]), FavoriteGenre = Convert.ToString(record["favouritegenre"])
                    };
            }
        });
    }

    public void AddUser(User input) {
        ExecuteWithDbConnection(connection => {
            using var cmd = new NpgsqlCommand("INSERT INTO users(username, password) VALUES (@username, @password)", connection);
            cmd.Parameters.AddWithValue("username", input.Username!);
            cmd.Parameters.AddWithValue("password", input.Password!);
            cmd.ExecuteNonQuery();
            return true;
        });
    }

    public void SetUserPreferences(User input) {
        ExecuteWithDbConnection(connection => {
            if (input.EMail != null) {
                using var cmd = new NpgsqlCommand("UPDATE users SET email = @email WHERE userid=@userid", connection);
                cmd.Parameters.AddWithValue("email", input.EMail);
                cmd.Parameters.AddWithValue("userid", input.Id!);
                cmd.ExecuteNonQuery();
            }
            if (input.FavoriteGenre != null) {
                using var cmd = new NpgsqlCommand("UPDATE users SET favouritegenre = @favouritegenre WHERE userid=@userid", connection);
                cmd.Parameters.AddWithValue("favouritegenre", input.FavoriteGenre);
                cmd.Parameters.AddWithValue("userid", input.Id!);
                cmd.ExecuteNonQuery();
            }
            return true;
        });
    }
}
