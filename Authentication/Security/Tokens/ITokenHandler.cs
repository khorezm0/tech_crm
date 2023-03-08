using Authentication.Models.Authentication;
using Authentication.Models.Tokens;

namespace Authentication.Security.Tokens;

public interface ITokenHandler
{
     AccessToken CreateAccessToken(User user);
     RefreshToken? TakeRefreshToken(User user, string token);
     void RevokeRefreshToken(User user, string token);
}