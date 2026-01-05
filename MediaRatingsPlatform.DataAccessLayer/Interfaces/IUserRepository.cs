using MediaRatingsPlatform.SharedObjects;

namespace MediaRatingsPlatform.DataAccessLayer.Interfaces;

public interface IUserRepository : IRepository<User> {
    public User? GetUser(User input);
    public void AddUser(User input);
    public void SetUserPreferences(User input);
}
