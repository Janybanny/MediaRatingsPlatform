namespace MediaRatingsPlatform.SharedObjects;

public class Token : IModel {
    public string? Id { get; set; }
    public int? UserId { get; set; }
    public DateTime? ValidUntil { get; set; }
}
