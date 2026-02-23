using MediaRatingsPlatform.DataAccessLayer.Interfaces;
using MediaRatingsPlatform.SharedObjects;

namespace MediaRatingsPlatform.BusinessLayer.Authentication;

public class RatingAuth : IAuth {
    public int? UserId { get; set; }

    public void Auth(Token token, int? comparator, IRepositoryFactory repositoryFactory) {
        // throws error when token is not valid or token userId is not comparator rating owner userId
        // throws error when token is not valid or token userId is not comparator media owner userId
        var db = repositoryFactory.CreateTokenRepository();
        var verifiedToken = db.GetToken(token);
        if (verifiedToken == null) throw new ApiBadLoginDataException();
        if (verifiedToken.ValidUntil < DateTime.Now) {
            db.DeleteToken(verifiedToken);
            throw new ApiTokenExpiredException();
        }
        var ratingdb = repositoryFactory.CreateRatingRepository();
        var rating = ratingdb.GetRatingById(new Rating { Id = comparator });
        if (rating == null) throw new ApiItemDoesNotExistException();
        if (verifiedToken.UserId != rating.Owner) throw new ApiNoAccessException();
        UserId = verifiedToken.UserId;
    }
}
