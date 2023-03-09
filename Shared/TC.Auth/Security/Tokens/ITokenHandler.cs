using TC.Auth.Models.Authentication;
using TC.Auth.Models.Tokens;

namespace TC.Auth.Security.Tokens;

public interface ITokenHandler
{
     AccessToken CreateAccessToken(User user);
     RefreshToken TakeRefreshToken(User user, string token);
     void RevokeRefreshToken(User user, string token);
}