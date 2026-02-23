namespace MediaRatingsPlatform.SharedObjects;

public class Like : IModel {
    public int? UserId { get; set; }
    public int? RatingId { get; set; }
}
