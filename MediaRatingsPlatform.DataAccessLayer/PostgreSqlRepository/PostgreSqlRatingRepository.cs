using MediaRatingsPlatform.DataAccessLayer.Interfaces;
using MediaRatingsPlatform.SharedObjects;

namespace MediaRatingsPlatform.DataAccessLayer.PostgreSqlRepository;

public class PostgreSqlRatingRepository(string connectionString) : PostgreSqlBaseRepository(connectionString), IRatingRepository {
    public Rating? GetRating(Rating input) {
        throw new ApiNotImplementedException();
    }
}
