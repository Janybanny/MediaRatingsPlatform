namespace MediaRatingsPlatform.SharedObjects;

public class Favourite : IModel {
    public int? UserId { get; set; }
    public int? MediaId { get; set; }
}
