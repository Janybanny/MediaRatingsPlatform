using MediaRatingsPlatform.BusinessLayer.Interfaces;
using MediaRatingsPlatform.DataAccessLayer.Interfaces;
using MediaRatingsPlatform.SharedObjects;

namespace MediaRatingsPlatform.BusinessLayer;

public class Authenticator(IRepositoryFactory database) : IAuthenticator {
    private readonly IRepositoryFactory _database = database;

    public void Register(User logInData) {
        // registers user
        var db = _database.CreateUserRepository();
        if (db.GetUser(logInData) != null) throw new ApiUserAlreadyExistsException();
        db.AddUser(logInData);
    }

    public string Login(User logInData) {
        // checks if user already exists
        var userdb = _database.CreateUserRepository();
        var validUser = userdb.GetUser(logInData);
        if (validUser == null) throw new ApiBadLoginDataException();
        // checks if password is valid
        if (logInData.Password != validUser.Password) throw new ApiBadLoginDataException();
        // checks if token already exists to reuse and extend lifetime. Deletes token if not valid anymore
        var tokendb = _database.CreateTokenRepository();
        var token = tokendb.GetToken(new Token { UserId = validUser.Id });
        if (token != null) {
            if (token.ValidUntil > DateTime.Now) {
                token.ValidUntil = DateTime.Now.AddHours(4);
                tokendb.UpdateToken(token);
                return token.Id!;
            }
            tokendb.DeleteToken(token);
        }
        // creates new token if one doesn't exist
        token = new Token { UserId = validUser.Id, ValidUntil = DateTime.Now.AddHours(4) };
        do {
            token.Id = Guid.NewGuid().ToString();
        } while (tokendb.GetToken(token) != null);
        tokendb.AddToken(token);
        // returns token string 
        return token.Id;
    }
}
