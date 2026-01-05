using MediaRatingsPlatform.SharedObjects;

namespace MediaRatingsPlatform.DataAccessLayer.Interfaces;

public interface ITokenRepository : IRepository<Token> {
    public Token? GetToken(Token input);
    public void SetToken(Token input);
    public void DeleteToken(Token input);
}
