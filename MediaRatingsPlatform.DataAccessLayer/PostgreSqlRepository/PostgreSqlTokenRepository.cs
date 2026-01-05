using System.Data;
using MediaRatingsPlatform.DataAccessLayer.Interfaces;
using MediaRatingsPlatform.SharedObjects;
using Npgsql;

namespace MediaRatingsPlatform.DataAccessLayer.PostgreSqlRepository;

public class PostgreSqlTokenRepository : PostgreSqlBaseRepository, ITokenRepository {
    public Token? GetToken(Token input) {
        return ExecuteWithDbConnection(connection => {
            IDataRecord? record = null;
            // uses Id, falls back to Username if no Id provided
            if (input.Id == null) {
                using var cmd = new NpgsqlCommand("SELECT * FROM tokens WHERE userid=@userid", connection);
                cmd.Parameters.AddWithValue("userid", input.UserId!);
                using var reader = cmd.ExecuteReader();
                if (reader.Read()) record = reader;
            } else {
                using var cmd = new NpgsqlCommand("SELECT * FROM tokens WHERE token=@token", connection);
                cmd.Parameters.AddWithValue("token", input.Id!);
                using var reader = cmd.ExecuteReader();
                if (reader.Read()) record = reader;
            }

            return record == null
                ? null
                : new Token {
                    Id = Convert.ToString(record["token"]), UserId = Convert.ToInt32(record["userid"]), ValidUntil = Convert.ToDateTime(record["validuntil"])
                };
        });
    }

    public void SetToken(Token input) {
        ExecuteWithDbConnection(connection => {
            // checks if it should use INSERT or UPDATE
            using var cmd = new NpgsqlCommand("SELECT token FROM tokens WHERE token=@token", connection);
            cmd.Parameters.AddWithValue("token", input.Id!);
            if (cmd.ExecuteNonQuery() == 0) {
                using var cmd2 = new NpgsqlCommand("INSERT INTO tokens(token, userid, validuntil) VALUES (@token, @userid, @vailduntil)", connection);
                cmd.Parameters.AddWithValue("token", input.Id!);
                cmd.Parameters.AddWithValue("userid", input.UserId!);
                cmd.Parameters.AddWithValue("validuntil", input.ValidUntil?.ToString("yyyy-MM-dd HH:mm:ss.fff")!);
                return cmd2.ExecuteNonQuery();
            } else {
                using var cmd2 = new NpgsqlCommand("UPDATE tokens SET validuntil = @validuntil WHERE token=@token", connection);
                cmd.Parameters.AddWithValue("token", input.Id!);
                cmd.Parameters.AddWithValue("token", input.Id!);
                return cmd2.ExecuteNonQuery();
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
}
