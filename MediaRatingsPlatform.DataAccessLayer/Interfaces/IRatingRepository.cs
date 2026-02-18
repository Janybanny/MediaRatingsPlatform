using MediaRatingsPlatform.SharedObjects;

namespace MediaRatingsPlatform.DataAccessLayer.Interfaces;

public interface IRatingRepository : IRepository<Rating> {
    public Rating? GetRating(Rating input);
}
