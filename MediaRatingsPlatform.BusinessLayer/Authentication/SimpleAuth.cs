using MediaRatingsPlatform.DataAccessLayer.Interfaces;
using MediaRatingsPlatform.SharedObjects;

namespace MediaRatingsPlatform.BusinessLayer.Authentication;

public class SimpleAuth : IAuth {
    public int? UserId { get; set; }

    public void Auth(Token token, int? comparator, IRepositoryFactory repositoryFactory) {
        // throws error when token is not valid
        var db = repositoryFactory.CreateTokenRepository();
        var verifiedToken = db.GetToken(token);
        if (verifiedToken == null) throw new ApiBadLoginDataException();
        if (verifiedToken.ValidUntil < DateTime.Now) {
            db.DeleteToken(verifiedToken);
            throw new ApiTokenExpiredException();
        }
        UserId = verifiedToken.UserId;
    }
}
