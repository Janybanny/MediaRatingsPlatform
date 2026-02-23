namespace MediaRatingsPlatform.SharedObjects;

public class User : IModel {
    public int? Id { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public string? EMail { get; set; }
    public string? FavoriteGenre { get; set; }
    public int? TotalMediaStatistic { get; set; }
    public int? TotalRatingsStatistic { get; set; }
    public double? AverageRatingStatistic { get; set; }
}
