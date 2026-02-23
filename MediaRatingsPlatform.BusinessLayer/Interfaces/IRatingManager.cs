using MediaRatingsPlatform.SharedObjects;

namespace MediaRatingsPlatform.BusinessLayer.Interfaces;

public interface IRatingManager {
    public void RateMedia(Rating rating);
    public void ComfirmRatingComment(Rating rating);
    public void LikeRating(Like like);
    public void UpdateRating(Rating rating);
    public void DeleteRating(Rating rating);
    public List<Rating> GetUserRatings(User user);
}
