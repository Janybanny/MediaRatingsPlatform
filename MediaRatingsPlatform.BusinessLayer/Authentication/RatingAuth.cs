using MediaRatingsPlatform.DataAccessLayer.Interfaces;
using MediaRatingsPlatform.SharedObjects;

namespace MediaRatingsPlatform.Authentication;

public class RatingAuth : IAuth {
    public int? UserId { get; set; }

    public void Auth(Token token, int? comparator, IRepositoryFactory repositoryFactory) {
        // throws error when token is not valid or token userId is not comparator rating owner userId
        var db = repositoryFactory.CreateTokenRepository();
        var verifiedToken = db.GetToken(token);
        if (verifiedToken == null) throw new ApiBadLoginDataException();
        if (comparator != verifiedToken.UserId) throw new ApiNoAccessException();
        var ratingdb = repositoryFactory.CreateRatingRepository();
        var rating = ratingdb.GetRating(new Rating { Id = comparator });
        if (rating == null) throw new ApiItemDoesNotExistException();
        if (verifiedToken.UserId != rating.Owner) throw new ApiNoAccessException();
        UserId = verifiedToken.UserId;
    }
}
