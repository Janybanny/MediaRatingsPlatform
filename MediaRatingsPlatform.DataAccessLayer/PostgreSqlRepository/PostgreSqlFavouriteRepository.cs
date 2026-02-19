using MediaRatingsPlatform.DataAccessLayer.Interfaces;
using MediaRatingsPlatform.SharedObjects;
using Npgsql;

namespace MediaRatingsPlatform.DataAccessLayer.PostgreSqlRepository;

public class PostgreSqlFavouriteRepository(string connectionString) : PostgreSqlBaseRepository(connectionString), IFavouriteRepository {
    public void Favourite(Favourite input) {
        ExecuteWithDbConnection(connection => {
            using var cmd = new NpgsqlCommand("INSERT INTO favourites(userId, mediaId) VALUES (@userId, @mediaId)", connection);
            cmd.Parameters.AddWithValue("userId", input.UserId!);
            cmd.Parameters.AddWithValue("mediaId", input.MediaId!);
            cmd.ExecuteNonQuery();
            return true;
        });
    }

    public void Unfavourite(Favourite input) {
        ExecuteWithDbConnection(connection => {
            using var cmd = new NpgsqlCommand("DELETE FROM favourites WHERE userId=@userId AND mediaId=@mediaId", connection);
            cmd.Parameters.AddWithValue("userId", input.UserId!);
            cmd.Parameters.AddWithValue("mediaId", input.MediaId!);
            cmd.ExecuteNonQuery();
            return true;
        });
    }

    public List<Favourite> GetFavourites(User input) {
        return ExecuteWithDbConnection<List<Favourite>>(connection => {
            using var cmd = new NpgsqlCommand("SELECT * FROM favourites WHERE userId=@userId", connection);
            cmd.Parameters.AddWithValue("userId", input.Id!);
            List<Favourite> favourites = [];
            using var reader = cmd.ExecuteReader();
            while (reader.Read()) favourites.Add(new Favourite { MediaId = Convert.ToInt32(reader["userId"]), UserId = Convert.ToInt32(reader["mediaId"]) });
            return favourites;
        });
    }
}
