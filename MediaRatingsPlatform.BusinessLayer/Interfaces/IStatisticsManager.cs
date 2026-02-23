using MediaRatingsPlatform.SharedObjects;

namespace MediaRatingsPlatform.BusinessLayer.Interfaces;

public interface IStatisticsManager {
    public List<LeaderboardNameEntry> GetLeaderboard();
}
