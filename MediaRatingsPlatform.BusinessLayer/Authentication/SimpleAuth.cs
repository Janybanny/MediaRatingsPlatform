using MediaRatingsPlatform.Models;

namespace MediaRatingsPlatform.Authentication;

public class SimpleAuth : IAuth {
    public string Auth(string token, string comparator) {
        // TODO throw error when auth key is not authenticate
        throw new ApiNotImplementedException();
        return "TEST";
    }
}