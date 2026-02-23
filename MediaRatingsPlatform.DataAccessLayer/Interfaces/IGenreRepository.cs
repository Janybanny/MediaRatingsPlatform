using MediaRatingsPlatform.SharedObjects;

namespace MediaRatingsPlatform.DataAccessLayer.Interfaces;

public interface IGenreRepository : IRepository<Genre> {
    public void AddGenre(Genre input);
    public void RemoveGenre(Genre input);
    public List<Genre> GetGenres(Genre input);
}
