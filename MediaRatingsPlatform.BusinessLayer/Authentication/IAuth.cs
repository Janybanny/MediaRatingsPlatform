using MediaRatingsPlatform.SharedObjects;

namespace MediaRatingsPlatform.Authentication;

public interface IAuth {
    string Auth(Token token, string comparator);
}