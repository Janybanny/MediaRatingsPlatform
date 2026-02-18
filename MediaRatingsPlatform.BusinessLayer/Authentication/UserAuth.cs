using MediaRatingsPlatform.DataAccessLayer.Interfaces;
using MediaRatingsPlatform.SharedObjects;

namespace MediaRatingsPlatform.Authentication;

public class UserAuth : IAuth {
    public int? UserId { get; set; }

    public void Auth(Token token, int? comparator, IRepositoryFactory repositoryFactory) {
        // throws error when token is not valid or token userId is not comparator userId
        var db = repositoryFactory.CreateTokenRepository();
        var verifiedToken = db.GetToken(token);
        if (verifiedToken == null) throw new ApiBadLoginDataException();
        if (comparator != verifiedToken.UserId) throw new ApiNoAccessException();
        UserId = verifiedToken.UserId;
    }
}
