using MediaRatingsPlatform.BusinessLayer.Interfaces;
using MediaRatingsPlatform.DataAccessLayer.Interfaces;
using MediaRatingsPlatform.SharedObjects;

namespace MediaRatingsPlatform.BusinessLayer;

public class UserManager(IRepositoryFactory database) : IUserManager {
    public User GetProfile(User input) {
        var db = database.CreateUserRepository();
        var user = db.GetUser(input);
        user!.TotalMediaStatistic = database.CreateMediaRepository().CountMediaByUser(user);
        var ratings = database.CreateRatingRepository().GetRatingsByUser(user);
        var counter = 0;
        var total = 0;
        foreach (var rating in ratings)
            if (rating.Stars != null) {
                counter++;
                total += (int)rating.Stars;
            }
        user.TotalRatingsStatistic = counter;
        user.AverageRatingStatistic = counter == 0 ? 0 : total / counter;
        user.Password = null;
        return user;
    }

    public void UpdateProfile(User input) {
        var db = database.CreateUserRepository();
        db.SetUserPreferences(input);
    }
}
