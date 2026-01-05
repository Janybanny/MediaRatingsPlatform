using System.Data;
using MediaRatingsPlatform.DataAccessLayer.Interfaces;
using MediaRatingsPlatform.SharedObjects;
using Npgsql;

namespace MediaRatingsPlatform.DataAccessLayer.PostgreSqlRepository;

public class PostgreSqlUserRepository : PostgreSqlBaseRepository, IUserRepository {
    public User? GetUser(User input) {
        return ExecuteWithDbConnection(connection => {
            IDataRecord? record = null;
            // uses Id, falls back to Username if no Id provided
            if (input.Id == null) {
                using var cmd = new NpgsqlCommand("SELECT * FROM users WHERE username=@username", connection);
                cmd.Parameters.AddWithValue("username", input!);
                using var reader = cmd.ExecuteReader();
                if (reader.Read()) record = reader;
            } else {
                using var cmd = new NpgsqlCommand("SELECT * FROM users WHERE userid=@userid", connection);
                cmd.Parameters.AddWithValue("token", input.Id!);
                using var reader = cmd.ExecuteReader();
                if (reader.Read()) record = reader;
            }

            return record == null
                ? null
                : new User {
                    Id = Convert.ToInt32(record["userid"]), Username = Convert.ToString(record["username"]), Password = Convert.ToString(record["password"]), EMail = Convert.ToString(record["email"]), FavouriteGenre = Convert.ToString(record["favouritegenre"])
                };
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
                using var cmd = new NpgsqlCommand("UPDATE users SET email = @email WHERE username=@username", connection);
                cmd.Parameters.AddWithValue("email", input.EMail);
                cmd.Parameters.AddWithValue("username", input.Username!);
                cmd.ExecuteNonQuery();
            }
            if (input.FavouriteGenre != null) {
                using var cmd = new NpgsqlCommand("UPDATE users SET favouritegenre = @favouritegenre WHERE username=@username", connection);
                cmd.Parameters.AddWithValue("favouritegenre", input.FavouriteGenre);
                cmd.Parameters.AddWithValue("username", input.Username!);
                cmd.ExecuteNonQuery();
            }
            return true;
        });
    }
}
