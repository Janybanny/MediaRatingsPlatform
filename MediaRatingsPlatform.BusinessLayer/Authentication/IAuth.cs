using MediaRatingsPlatform.SharedObjects;

namespace MediaRatingsPlatform.Authentication;

public interface IAuth {
    public int? UserId { get; set; }
    void Auth(Token token, string comparator);
}
