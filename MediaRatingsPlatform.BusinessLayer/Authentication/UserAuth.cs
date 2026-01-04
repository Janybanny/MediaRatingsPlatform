using MediaRatingsPlatform.Models;

namespace MediaRatingsPlatform.Authentication;

public class UserAuth : IAuth {
    public string Auth(string token, string comparator) {
        throw new ApiNotImplementedException();

    }
}