using MediaRatingsPlatform.SharedObjects;

namespace MediaRatingsPlatform.Authentication;

public class MediaAuth : IAuth {
    public int? UserId { get; set; } = null;

    public void Auth(Token token, string comparator) {
        throw new ApiNotImplementedException();
    }
}
