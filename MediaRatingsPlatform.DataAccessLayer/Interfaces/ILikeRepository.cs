using MediaRatingsPlatform.SharedObjects;

namespace MediaRatingsPlatform.DataAccessLayer.Interfaces;

public interface ILikeRepository : IRepository<Like> {
    void LikeRating(Like input);
    public bool GetLike(Like input);
}
