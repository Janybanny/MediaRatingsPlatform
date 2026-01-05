using MediaRatingsPlatform.DataAccessLayer.Interfaces;
using MediaRatingsPlatform.DataAccessLayer.PostgreSqlRepository;
using MediaRatingsPlatform.SharedObjects;

namespace MediaRatingsPlatform.Authentication;

public class Authenticator {
    public static void Register(User logInData) {
        // registers user
        IUserRepository db = new PostgreSqlUserRepository();
        if (db.GetUser(logInData) != null) throw new ApiUserAlreadyExistsException();
        db.AddUser(logInData);
    }

    public static string Login(User logInData) {
        // checks if user already exists
        IUserRepository userdb = new PostgreSqlUserRepository();
        var validUser = userdb.GetUser(logInData);
        if (validUser == null) throw new ApiBadLoginDataException();
        // checks if password is valid
        if (logInData.Password != validUser.Password) throw new ApiBadLoginDataException();
        // checks if token already exists to reuse and extend lifetime. Deletes token if not valid anymore
        ITokenRepository tokendb = new PostgreSqlTokenRepository();
        var token = tokendb.GetToken(new Token { UserId = validUser.Id });
        if (token != null) {
            if (token.ValidUntil > DateTime.Now) {
                token.ValidUntil = DateTime.Now.AddHours(4);
                tokendb.SetToken(token);
                return token.Id!;
            }
            tokendb.DeleteToken(token);
            token = null;
        }
        // creates new token if one doesn't exist
        token = new Token { UserId = validUser.Id, ValidUntil = DateTime.Now.AddHours(4) };
        do {
            token.Id = Guid.NewGuid().ToString();
        } while (tokendb.GetToken(token) != null);
        tokendb.SetToken(token);
        // returns token string 
        return token.Id;
    }
}
