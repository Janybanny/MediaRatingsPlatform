using MediaRatingsPlatform.SharedObjects;

namespace MediaRatingsPlatform.Authentication;

public class UserAuth : IAuth {
    public string Auth(Token token, string comparator) {
        throw new ApiNotImplementedException();
    }
}