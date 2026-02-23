using MediaRatingsPlatform.DataAccessLayer.Interfaces;
using MediaRatingsPlatform.SharedObjects;
using Npgsql;

namespace MediaRatingsPlatform.DataAccessLayer.PostgreSqlRepository;

public class PostgreSqlMediaRepository(string connectionString) : PostgreSqlBaseRepository(connectionString), IMediaRepository {
    public Media? GetMedia(Media input) {
        return ExecuteWithDbConnection<Media>(connection => {
            using var cmd = new NpgsqlCommand("SELECT * FROM media WHERE id=@id", connection);
            cmd.Parameters.AddWithValue("id", input.Id!);
            using var reader = cmd.ExecuteReader();
            if (!reader.Read()) return null;
            return new Media {
                Id = Convert.ToInt32(reader["id"]), Owner = Convert.ToInt32(reader["owner"]), AgeRestriction = Convert.ToInt32(reader["ageRestriction"]), Description = Convert.ToString(reader["description"]), MediaType = Convert.ToString(reader["mediaType"]), ReleaseYear = Convert.ToInt32(reader["releaseYear"]), Title = Convert.ToString(reader["title"])
            };
        });
    }

    public int AddMedia(Media input) {
        return ExecuteWithDbConnection(connection => {
            using var cmd = new NpgsqlCommand("INSERT INTO media(owner, title, description, mediaType, releaseYear, ageRestriction) VALUES (@owner, @title, @description, @mediaType, @releaseYear, @ageRestriction) RETURNING id", connection);
            cmd.Parameters.AddWithValue("owner", input.Owner!);
            cmd.Parameters.AddWithValue("title", input.Title!);
            cmd.Parameters.AddWithValue("description", input.Description!);
            cmd.Parameters.AddWithValue("mediaType", input.MediaType!);
            cmd.Parameters.AddWithValue("releaseYear", input.ReleaseYear!);
            cmd.Parameters.AddWithValue("ageRestriction", input.AgeRestriction!);
            return (int)cmd.ExecuteScalar()!;
        });
    }

    public void DeleteMedia(Media input) {
        ExecuteWithDbConnection(connection => {
            using var cmd = new NpgsqlCommand("DELETE FROM media WHERE id=@id", connection);
            cmd.Parameters.AddWithValue("id", input.Id!);
            cmd.ExecuteNonQuery();
            return true;
        });
    }

    public void UpdateMedia(Media input) {
        ExecuteWithDbConnection(connection => {
            using var cmd = new NpgsqlCommand("UPDATE media SET title = @title, description = @description, mediaType = @mediaType, releaseYear = @releaseYear, ageRestriction = @ageRestriction WHERE id=@id", connection);
            cmd.Parameters.AddWithValue("id", input.Id!);
            cmd.Parameters.AddWithValue("title", input.Title!);
            cmd.Parameters.AddWithValue("description", input.Description!);
            cmd.Parameters.AddWithValue("mediaType", input.MediaType!);
            cmd.Parameters.AddWithValue("releaseYear", input.ReleaseYear!);
            cmd.Parameters.AddWithValue("ageRestriction", input.AgeRestriction!);
            cmd.ExecuteNonQuery();
            return true;
        });
    }
}
