using MediaRatingsPlatform.DataAccessLayer.Interfaces;
using MediaRatingsPlatform.SharedObjects;

namespace MediaRatingsPlatform.DataAccessLayer.PostgreSqlRepository;

public class PostgreSqlMediaRepository(string connectionString) : PostgreSqlBaseRepository(connectionString), IMediaRepository {
    public Media? GetMedia(Media input) {
        throw new ApiNotImplementedException();
    }
}
