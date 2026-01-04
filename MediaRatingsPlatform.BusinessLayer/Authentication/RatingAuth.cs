using MediaRatingsPlatform.Models;

namespace MediaRatingsPlatform.Authentication;

public class RatingAuth : IAuth {
    public string Auth(string token, string comparator)
    {
        throw new ApiNotImplementedException();
    }
}