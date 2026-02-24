using MediaRatingsPlatform.DataAccessLayer.Interfaces;
using MediaRatingsPlatform.SharedObjects;
using Npgsql;

namespace MediaRatingsPlatform.DataAccessLayer.PostgreSqlRepository;

public class PostgreSqlGenreRepository(string connectionString) : PostgreSqlBaseRepository(connectionString), IGenreRepository {
    public void AddGenre(Genre input) {
        ExecuteWithDbConnection(connection => {
            using var cmd = new NpgsqlCommand("INSERT INTO genres(mediaid, genre) VALUES (@mediaid, @genre) ON CONFLICT DO NOTHING", connection);
            cmd.Parameters.AddWithValue("mediaid", input.MediaId!);
            cmd.Parameters.AddWithValue("genre", input.Name!);
            cmd.ExecuteNonQuery();
            return true;
        });
    }

    public void RemoveGenre(Genre input) {
        ExecuteWithDbConnection(connection => {
            using var cmd = new NpgsqlCommand("DELETE FROM genres WHERE mediaId=@mediaId AND genre=@genre", connection);
            cmd.Parameters.AddWithValue("mediaid", input.MediaId!);
            cmd.Parameters.AddWithValue("genre", input.Name!);
            cmd.ExecuteNonQuery();
            return true;
        });
    }

    public List<Genre> GetGenres(Genre input) {
        return ExecuteWithDbConnection<List<Genre>>(connection => {
            using var cmd = new NpgsqlCommand("SELECT * FROM genres WHERE mediaId=@mediaId", connection);
            cmd.Parameters.AddWithValue("mediaid", input.MediaId!);
            List<Genre> genres = [];
            using var reader = cmd.ExecuteReader();
            while (reader.Read()) genres.Add(new Genre { MediaId = Convert.ToInt32(reader["mediaid"]), Name = Convert.ToString(reader["genre"]) });
            return genres;
        });
    }

    public List<Genre> GetAllGenreEntries() {
        return ExecuteWithDbConnection<List<Genre>>(connection => {
            using var cmd = new NpgsqlCommand("SELECT * FROM genres", connection);
            using var reader = cmd.ExecuteReader();
            List<Genre> genres = [];
            while (reader.Read())
            while (reader.Read())
                genres.Add(new Genre { MediaId = Convert.ToInt32(reader["mediaid"]), Name = Convert.ToString(reader["genre"]) });
            return genres;
        });
    }
}
