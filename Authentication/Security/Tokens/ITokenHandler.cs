using Authentication.Models.Tokens;
using Business.Abstractions;

namespace Authentication.Security.Tokens;

public interface ITokenHandler
{
     AccessToken CreateAccessToken(User user);
     RefreshToken TakeRefreshToken(string token, int userId);
     void RevokeRefreshToken(string token, int userId);
}