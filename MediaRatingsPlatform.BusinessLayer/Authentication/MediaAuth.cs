using MediaRatingsPlatform.DataAccessLayer.Interfaces;
using MediaRatingsPlatform.SharedObjects;

namespace MediaRatingsPlatform.BusinessLayer.Authentication;

public class MediaAuth : IAuth {
    public int? UserId { get; set; }

    public void Auth(Token token, int? comparator, IRepositoryFactory repositoryFactory) {
        // throws error when token is not valid or token userId is not comparator media owner userId
        var db = repositoryFactory.CreateTokenRepository();
        var verifiedToken = db.GetToken(token);
        if (verifiedToken == null) throw new ApiBadLoginDataException();
        if (comparator != verifiedToken.UserId) throw new ApiNoAccessException();
        var mediadb = repositoryFactory.CreateMediaRepository();
        var media = mediadb.GetMedia(new Media { Id = comparator });
        if (media == null) throw new ApiItemDoesNotExistException();
        if (verifiedToken.UserId != media.Owner) throw new ApiNoAccessException();
        UserId = verifiedToken.UserId;
    }
}
