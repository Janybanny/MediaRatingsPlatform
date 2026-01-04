using MediaRatingsPlatform.SharedObjects;

namespace MediaRatingsPlatform.Authentication;

public class NoAuth : IAuth {
    public string Auth(Token token, string comparator) {
        return "";
    }
}