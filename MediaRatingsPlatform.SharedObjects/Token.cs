namespace MediaRatingsPlatform.SharedObjects;

public class Token : IModel {
    public string? Id { get; set; }
    public string? User { get; set; }
    public string? ValidUntil { get; set; }
}
