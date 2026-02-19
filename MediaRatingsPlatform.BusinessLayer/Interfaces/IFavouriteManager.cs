using MediaRatingsPlatform.SharedObjects;

namespace MediaRatingsPlatform.BusinessLayer.Interfaces;

public interface IFavouriteManager {
    public void Favourite(Favourite favourite);
    public void UnFavourite(Favourite favourite);
    public List<Media> GetFavourites(User user);
}
