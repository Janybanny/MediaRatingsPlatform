namespace MediaRatingsPlatform.Authentication;

public interface IAuth
{
    string Auth(string token, string comparator);
}