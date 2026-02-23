using MediaRatingsPlatform.DataAccessLayer.Interfaces;
using MediaRatingsPlatform.SharedObjects;
using Npgsql;

namespace MediaRatingsPlatform.DataAccessLayer.PostgreSqlRepository;

public class PostgreSqlLikeRepository(string connectionString) : PostgreSqlBaseRepository(connectionString), ILikeRepository {
    public void LikeRating(Like input) {
        ExecuteWithDbConnection(connection => {
            using var cmd = new NpgsqlCommand("INSERT INTO likes(userid, ratingid) VALUES (@userid, @ratingid) ON CONFLICT DO NOTHING", connection);
            cmd.Parameters.AddWithValue("userid", input.UserId!);
            cmd.Parameters.AddWithValue("ratingid", input.RatingId!);
            cmd.ExecuteNonQuery();
            return true;
        });
    }

    public bool GetLike(Like input) {
        return ExecuteWithDbConnection(connection => {
            using var cmd = new NpgsqlCommand("SELECT COUNT(*) FROM likes WHERE userid=@userid AND ratingid=@ratingid", connection);
            cmd.Parameters.AddWithValue("userid", input.UserId!);
            cmd.Parameters.AddWithValue("ratingid", input.RatingId!);
            var lines = cmd.ExecuteScalar();
            return Convert.ToInt32(lines) > 0;
        });
    }
}
