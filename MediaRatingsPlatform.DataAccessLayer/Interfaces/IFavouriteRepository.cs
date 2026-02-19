using MediaRatingsPlatform.SharedObjects;

namespace MediaRatingsPlatform.DataAccessLayer.Interfaces;

public interface IFavouriteRepository : IRepository<Favourite> {
    public void Favourite(Favourite input);
    public void Unfavourite(Favourite input);
    public List<Favourite> GetFavourites(User input);
}
