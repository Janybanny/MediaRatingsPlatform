using MediaRatingsPlatform.DataAccessLayer.Interfaces;
using MediaRatingsPlatform.SharedObjects;

namespace MediaRatingsPlatform.BusinessLayer.Authentication;

public interface IAuth {
    public int? UserId { get; set; }
    void Auth(Token token, int? comparator, IRepositoryFactory repositoryFactory);
}
