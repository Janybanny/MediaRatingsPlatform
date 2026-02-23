using System.Data;
using MediaRatingsPlatform.DataAccessLayer.Interfaces;
using MediaRatingsPlatform.SharedObjects;
using Npgsql;

namespace MediaRatingsPlatform.DataAccessLayer.PostgreSqlRepository;

public class PostgreSqlRatingRepository(string connectionString) : PostgreSqlBaseRepository(connectionString), IRatingRepository {
    public Rating? GetRatingById(Rating input) {
        return ExecuteWithDbConnection<Rating?>(connection => {
            using var cmd = new NpgsqlCommand("SELECT * FROM ratings WHERE id=@id", connection);
            cmd.Parameters.AddWithValue("id", input.Id!);
            using var reader = cmd.ExecuteReader();
            if (!reader.Read()) return null;
            return new Rating {
                Id = Convert.ToInt32(reader["id"]), Owner = Convert.ToInt32(reader["userid"]), MediaId = Convert.ToInt32(reader["mediaid"]), CreatedAt = reader.GetDateTime("created_at"), Stars = Convert.ToInt32(reader["stars"]), Comment = Convert.ToString(reader["comment"]), CommentVisible = Convert.ToBoolean(reader["commentVisible"])
            };
        });
    }

    public Rating? GetRatingByMediaAndUser(Rating input) {
        return ExecuteWithDbConnection<Rating?>(connection => {
            using var cmd = new NpgsqlCommand("SELECT * FROM ratings WHERE userid=@userid AND mediaid=@mediaid", connection);
            cmd.Parameters.AddWithValue("userid", input.Owner!);
            cmd.Parameters.AddWithValue("mediaid", input.MediaId!);
            using var reader = cmd.ExecuteReader();
            if (!reader.Read()) return null;
            return new Rating {
                Id = Convert.ToInt32(reader["id"]), Owner = Convert.ToInt32(reader["userid"]), MediaId = Convert.ToInt32(reader["mediaid"]), CreatedAt = reader.GetDateTime("created_at"), Stars = Convert.ToInt32(reader["stars"]), Comment = Convert.ToString(reader["comment"]), CommentVisible = Convert.ToBoolean(reader["commentVisible"])
            };
        });
    }

    public int AddRating(Rating input) {
        return ExecuteWithDbConnection(connection => {
            using var cmd = new NpgsqlCommand("INSERT INTO ratings(userid, mediaid, created_at, stars, comment, commentVisible) VALUES (@userid, @mediaid, @created_at, @stars, @comment, @commentVisible) RETURNING id", connection);
            cmd.Parameters.AddWithValue("userid", input.Owner!);
            cmd.Parameters.AddWithValue("mediaid", input.MediaId!);
            cmd.Parameters.AddWithValue("created_at", input.CreatedAt!);
            cmd.Parameters.AddWithValue("stars", input.Stars!);
            cmd.Parameters.AddWithValue("comment", input.Comment!);
            cmd.Parameters.AddWithValue("commentVisible", input.CommentVisible!);
            return (int)cmd.ExecuteScalar()!;
        });
    }

    public void UpdateRating(Rating input) {
        ExecuteWithDbConnection(connection => {
            using var cmd = new NpgsqlCommand("UPDATE ratings SET created_at = @created_at, stars = @stars, comment = @comment, commentVisible = @commentVisible WHERE id = @id", connection);
            cmd.Parameters.AddWithValue("id", input.Id!);
            cmd.Parameters.AddWithValue("created_at", input.CreatedAt!);
            cmd.Parameters.AddWithValue("stars", input.Stars!);
            cmd.Parameters.AddWithValue("comment", input.Comment!);
            cmd.Parameters.AddWithValue("commentVisible", input.CommentVisible!);
            cmd.ExecuteNonQuery();
            return true;
        });
    }

    public void DeleteRating(Rating input) {
        ExecuteWithDbConnection(connection => {
            using var cmd = new NpgsqlCommand("DELETE FROM ratings WHERE id=@id", connection);
            cmd.Parameters.AddWithValue("id", input.Id!);
            cmd.ExecuteNonQuery();
            return true;
        });
    }

    public List<LeaderboardEntry> GetLeaderboard() {
        return ExecuteWithDbConnection(connection => {
            using var cmd = new NpgsqlCommand("SELECT userid, COUNT(*) AS rating_count FROM ratings GROUP BY userid ORDER BY rating_count DESC;", connection);
            using var reader = cmd.ExecuteReader();
            List<LeaderboardEntry> leaderboard = [];
            while (reader.Read()) leaderboard.Add(new LeaderboardEntry { user = Convert.ToInt32(reader["userid"]), ratings = Convert.ToInt32(reader["rating_count"]) });
            return leaderboard;
        });
    }

    public List<Rating> GetRatingsByUser(User input) {
        return ExecuteWithDbConnection<List<Rating>>(connection => {
            using var cmd = new NpgsqlCommand("SELECT * FROM ratings WHERE userid = @userid ORDER BY created_at;", connection);
            cmd.Parameters.AddWithValue("userid", input.Id!);
            using var reader = cmd.ExecuteReader();
            List<Rating> ratings = [];
            while (reader.Read())
                ratings.Add(new Rating {
                    Id = Convert.ToInt32(reader["id"]), Owner = Convert.ToInt32(reader["userid"]), MediaId = Convert.ToInt32(reader["mediaid"]), CreatedAt = reader.GetDateTime("created_at"), Stars = Convert.ToInt32(reader["stars"]), Comment = Convert.ToString(reader["comment"]), CommentVisible = Convert.ToBoolean(reader["commentVisible"])
                });
            return ratings;
        });
    }

    public List<Rating> GetRatingsByMedia(Media input) {
        return ExecuteWithDbConnection<List<Rating>>(connection => {
            using var cmd = new NpgsqlCommand("SELECT * FROM ratings WHERE mediaid = @mediaid ORDER BY created_at;", connection);
            cmd.Parameters.AddWithValue("mediaid", input.Id!);
            using var reader = cmd.ExecuteReader();
            List<Rating> ratings = [];
            while (reader.Read())
                ratings.Add(new Rating {
                    Id = Convert.ToInt32(reader["id"]), Owner = Convert.ToInt32(reader["userid"]), MediaId = Convert.ToInt32(reader["mediaid"]), CreatedAt = reader.GetDateTime("created_at"), Stars = Convert.ToInt32(reader["stars"]), Comment = Convert.ToString(reader["comment"]), CommentVisible = Convert.ToBoolean(reader["commentVisible"])
                });
            return ratings;
        });
    }
}

/*
        CREATE TABLE IF NOT EXISTS ratings (
          id integer PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
          userid integer NOT NULL REFERENCES users ON DELETE CASCADE,
          mediaid integer NOT NULL REFERENCES media ON DELETE CASCADE,
          created_at date NOT NULL,
          stars integer NOT NULL CHECK (stars > 0 AND stars < 6),
          comment text NOT NULL,
          commentVisible bool NOT NULL DEFAULT false
        );
*/
