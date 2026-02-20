using MediaRatingsPlatform.SharedObjects;

namespace MediaRatingsPlatform.DataAccessLayer.Interfaces;

public interface IMediaRepository : IRepository<Media> {
    public Media? GetMedia(Media input);
    public void AddMedia(Media input);
    public void DeleteMedia(Media media);
    public void UpdateMedia(Media media);
}
