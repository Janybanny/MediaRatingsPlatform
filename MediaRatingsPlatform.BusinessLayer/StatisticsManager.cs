using MediaRatingsPlatform.BusinessLayer.Interfaces;
using MediaRatingsPlatform.DataAccessLayer.Interfaces;
using MediaRatingsPlatform.SharedObjects;

namespace MediaRatingsPlatform.BusinessLayer;

public class StatisticsManager(IRepositoryFactory database) : IStatisticsManager {
    public List<LeaderboardNameEntry> GetLeaderboard() {
        var idLeaderboard = database.CreateRatingRepository().GetLeaderboard();
        var userdb = database.CreateUserRepository();
        List<LeaderboardNameEntry> leaderboard = [];
        foreach (var entry in idLeaderboard) leaderboard.Add(new LeaderboardNameEntry { Ratings = entry.Ratings, User = userdb.GetUser(new User { Id = entry.User })!.Username! });
        return leaderboard;
    }
}
