using MediaRatingsPlatform.SharedObjects;

namespace MediaRatingsPlatform.DataAccessLayer.Interfaces;

public interface ITokenRepository : IRepository<Token> {
    public Token? GetToken(Token input);
    public void AddToken(Token input);
    public void UpdateToken(Token input);
    public void DeleteToken(Token input);
}
