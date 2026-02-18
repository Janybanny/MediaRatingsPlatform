using MediaRatingsPlatform.SharedObjects;

namespace MediaRatingsPlatform.DataAccessLayer.Interfaces;

public interface IMediaRepository : IRepository<Media> {
    public Media? GetMedia(Media input);
}
