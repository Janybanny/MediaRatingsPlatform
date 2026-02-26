namespace MediaRatingsPlatform.SharedObjects;

public class LeaderboardEntry : IModel {
    public int User { get; set; }
    public int Ratings { get; set; }
}

public class LeaderboardNameEntry : IModel {
    public string User { get; set; } = null!;
    public int Ratings { get; set; }
}
