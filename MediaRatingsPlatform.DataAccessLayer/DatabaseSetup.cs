using MediaRatingsPlatform.SharedObjects;
using Npgsql;

namespace MediaRatingsPlatform.DataAccessLayer;

public static class DatabaseSetup {
    private const string CreateTablesCommand = """
        CREATE TABLE IF NOT EXISTS users (
          userid integer PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
          username text UNIQUE NOT NULL,
          password text NOT NULL,
          email text,
          favouriteGenre text
        );

        CREATE TABLE IF NOT EXISTS tokens (
          token text PRIMARY KEY,
          userid integer UNIQUE NOT NULL REFERENCES users ON DELETE CASCADE,
          validuntil timestamp NOT NULL
        );

        CREATE TABLE IF NOT EXISTS media (
          id integer PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
          owner integer NOT NULL REFERENCES users ON DELETE CASCADE,
          title text NOT NULL,
          description text NOT NULL,
          mediaType text NOT NULL,
          releaseYear integer NOT NULL,
          ageRestriction integer NOT NULL
        );

        CREATE TABLE IF NOT EXISTS genres (
          mediaid integer REFERENCES media ON DELETE CASCADE,
          genre text NOT NULL,
          PRIMARY KEY(mediaId, genre)
        );

        CREATE TABLE IF NOT EXISTS favourites (
          userid integer REFERENCES users ON DELETE CASCADE,
          mediaid integer REFERENCES media ON DELETE CASCADE,
          PRIMARY KEY(userId, mediaId)
        );

        CREATE TABLE IF NOT EXISTS ratings (
          id integer PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
          userid integer NOT NULL REFERENCES users ON DELETE CASCADE,
          mediaid integer NOT NULL REFERENCES media ON DELETE CASCADE,
          created_at date NOT NULL,
          stars integer NOT NULL CHECK (stars > 0 AND stars < 6),
          comment text NOT NULL,
          commentVisible bool NOT NULL DEFAULT false
        );

        CREATE TABLE IF NOT EXISTS likes (
          userid integer REFERENCES users ON DELETE CASCADE,
          ratingid integer REFERENCES ratings ON DELETE CASCADE,
          PRIMARY KEY(userId, ratingId)
        );
        """;

    public static void EnsureTablesExist(string connectionString) {
        try {
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            using var cmd = new NpgsqlCommand(CreateTablesCommand, connection);
            cmd.ExecuteNonQuery();
        } catch (NpgsqlException e) {
            Console.WriteLine($"DBError: {e.Message}");
            throw new ApiBadLoginDataException();
        }
    }
}
