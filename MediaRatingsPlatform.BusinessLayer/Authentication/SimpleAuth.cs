using MediaRatingsPlatform.SharedObjects;

namespace MediaRatingsPlatform.Authentication;

public class SimpleAuth : IAuth {
    public string Auth(Token token, string comparator) {
        // TODO throw error when auth key is not authenticate
        throw new ApiNotImplementedException();
        ///Token? validatedToken = DB.GetToken(token);
        //return username;
    }
}