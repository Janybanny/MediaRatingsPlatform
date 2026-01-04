namespace MediaRatingsPlatform.Authentication;

public class NoAuth : IAuth
{
    public string Auth(string token, string comparator)
    {
        return "";
    }
}