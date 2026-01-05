using MediaRatingsPlatform.SharedObjects;

namespace MediaRatingsPlatform.Authentication;

public class RatingAuth : IAuth {
    public int? UserId { get; set; } = null;

    public void Auth(Token token, string comparator) {
        throw new ApiNotImplementedException();
    }
}
