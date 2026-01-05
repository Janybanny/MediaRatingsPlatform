using MediaRatingsPlatform.SharedObjects;

namespace MediaRatingsPlatform.Authentication;

public class NoAuth : IAuth {
    public int? UserId { get; set; } = null;

    public void Auth(Token token, string comparator) {
    }
}
