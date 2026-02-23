namespace MediaRatingsPlatform.SharedObjects;

public class LeaderboardEntry : IModel {
    public int user { get; set; }
    public int ratings { get; set; }
}

public class LeaderboardNameEntry : IModel {
    public string user { get; set; }
    public int ratings { get; set; }
}
