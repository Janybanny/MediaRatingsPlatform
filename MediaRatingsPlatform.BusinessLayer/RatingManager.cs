using MediaRatingsPlatform.BusinessLayer.Interfaces;
using MediaRatingsPlatform.DataAccessLayer.Interfaces;
using MediaRatingsPlatform.SharedObjects;

namespace MediaRatingsPlatform.BusinessLayer;

public class RatingManager(IRepositoryFactory database) : IRatingManager {
    public void RateMedia(Rating rating) {
        if (database.CreateMediaRepository().GetMedia(new Media { Id = rating.MediaId }) == null) throw new ApiItemDoesNotExistException();
        rating.CreatedAt = DateTime.Now;
        rating.Comment ??= "";
        rating.CommentVisible = false;
        var ratingdb = database.CreateRatingRepository();
        var verifiedrating = ratingdb.GetRatingByMediaAndUser(rating);
        if (verifiedrating == null) {
            ratingdb.AddRating(rating);
        } else {
            rating.Id = verifiedrating.Id;
            ratingdb.UpdateRating(rating);
        }
    }

    public void ComfirmRatingComment(Rating rating) {
        var ratingdb = database.CreateRatingRepository();
        var verifiedrating = ratingdb.GetRatingById(rating);
        if (verifiedrating == null) throw new ApiItemDoesNotExistException();
        verifiedrating.CommentVisible = true;
        ratingdb.UpdateRating(verifiedrating);
    }

    public void LikeRating(Like like) {
        if (database.CreateRatingRepository().GetRatingById(new Rating { Id = like.RatingId }) == null) throw new ApiItemDoesNotExistException();
        var likedb = database.CreateLikeRepository();
        likedb.LikeRating(like);
    }

    public void UpdateRating(Rating rating) {
        rating.CreatedAt = DateTime.Now;
        rating.Comment ??= "";
        rating.CommentVisible = false;
        var ratingdb = database.CreateRatingRepository();
        var verifiedrating = ratingdb.GetRatingById(rating);
        if (verifiedrating == null) throw new ApiItemDoesNotExistException();
        rating.MediaId = verifiedrating.MediaId;
        rating.Owner = verifiedrating.Owner;
        ratingdb.UpdateRating(rating);
    }

    public void DeleteRating(Rating rating) {
        database.CreateRatingRepository().DeleteRating(rating);
    }

    public List<Rating> GetUserRatings(User user) {
        return database.CreateRatingRepository().GetRatingsByUser(user);
    }
}
