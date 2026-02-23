using MediaRatingsPlatform.SharedObjects;

namespace MediaRatingsPlatform.DataAccessLayer.Interfaces;

public interface IRatingRepository : IRepository<Rating> {
    public Rating? GetRatingById(Rating input);
    public Rating? GetRatingByMediaAndUser(Rating input);
    public int AddRating(Rating input);
    public void UpdateRating(Rating input);
    public void DeleteRating(Rating rating);
    public List<LeaderboardEntry> GetLeaderboard();
    public List<Rating> GetRatingsByUser(User input);
    public List<Rating> GetRatingsByMedia(Media input);
}
