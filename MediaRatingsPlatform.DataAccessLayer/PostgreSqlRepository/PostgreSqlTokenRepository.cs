using MediaRatingsPlatform.DataAccessLayer.Interfaces;
using MediaRatingsPlatform.SharedObjects;
using Npgsql;

namespace MediaRatingsPlatform.DataAccessLayer.PostgreSqlRepository;

public class PostgreSqlTokenRepository(string connectionString) : PostgreSqlBaseRepository(connectionString), ITokenRepository {
    public Token? GetToken(Token input) {
        return ExecuteWithDbConnection(connection => {
            // uses Id, falls back to Username if no Id provided
            if (input.Id == null) {
                using var cmd = new NpgsqlCommand("SELECT * FROM tokens WHERE userid=@userid", connection);
                cmd.Parameters.AddWithValue("userid", input.UserId!);
                using var reader = cmd.ExecuteReader();
                return reader.Read()
                    ? new Token {
                        Id = Convert.ToString(reader["token"]), UserId = Convert.ToInt32(reader["userid"]), ValidUntil = Convert.ToDateTime(reader["validuntil"])
                    }
                    : null;
            } else {
                using var cmd = new NpgsqlCommand("SELECT * FROM tokens WHERE token=@token", connection);
                cmd.Parameters.AddWithValue("token", input.Id!);
                using var reader = cmd.ExecuteReader();
                return reader.Read()
                    ? new Token {
                        Id = Convert.ToString(reader["token"]), UserId = Convert.ToInt32(reader["userid"]), ValidUntil = Convert.ToDateTime(reader["validuntil"])
                    }
                    : null;
            }
        });
    }

    public void DeleteToken(Token input) {
        ExecuteWithDbConnection(connection => {
            // uses either Id or Username if Id not present
            if (input.Id == null) {
                using var cmd = new NpgsqlCommand("DELETE FROM tokens WHERE userid=@userid", connection);
                cmd.Parameters.AddWithValue("userid", input.UserId!);
                return cmd.ExecuteNonQuery();
            } else {
                using var cmd = new NpgsqlCommand("DELETE FROM tokens WHERE token=@token", connection);
                cmd.Parameters.AddWithValue("token", input.Id!);
                return cmd.ExecuteNonQuery();
            }
        });
    }

    public void AddToken(Token input) {
        ExecuteWithDbConnection(connection => {
            using var cmd = new NpgsqlCommand("INSERT INTO tokens(token, userid, validuntil) VALUES (@token, @userid, @validuntil)", connection);
            cmd.Parameters.AddWithValue("token", input.Id!);
            cmd.Parameters.AddWithValue("userid", input.UserId!);
            cmd.Parameters.AddWithValue("validuntil", input.ValidUntil!);
            return cmd.ExecuteNonQuery();
        });
    }

    public void UpdateToken(Token input) {
        ExecuteWithDbConnection(connection => {
            using var cmd = new NpgsqlCommand("UPDATE tokens SET validuntil = @validuntil WHERE token=@token", connection);
            cmd.Parameters.AddWithValue("token", input.Id!);
            cmd.Parameters.AddWithValue("validuntil", input.ValidUntil!);
            return cmd.ExecuteNonQuery();
        });
    }
}
