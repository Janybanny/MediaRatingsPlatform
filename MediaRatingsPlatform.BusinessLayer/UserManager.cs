using MediaRatingsPlatform.BusinessLayer.Interfaces;
using MediaRatingsPlatform.DataAccessLayer.Interfaces;
using MediaRatingsPlatform.SharedObjects;

namespace MediaRatingsPlatform.BusinessLayer;

public class UserManager(IRepositoryFactory database) : IUserManager {
    private readonly IRepositoryFactory _database = database;

    public User? GetProfile(User input) {
        var db = _database.CreateUserRepository();
        return db.GetUser(input);
    }

    public void UpdateProfile(User input) {
        var db = _database.CreateUserRepository();
        db.SetUserPreferences(input);
    }
}
