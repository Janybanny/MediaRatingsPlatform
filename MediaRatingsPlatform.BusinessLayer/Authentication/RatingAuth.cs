using MediaRatingsPlatform.SharedObjects;

namespace MediaRatingsPlatform.Authentication;

public class RatingAuth : IAuth {
    public string Auth(Token token, string comparator) {
        throw new ApiNotImplementedException();
    }
}