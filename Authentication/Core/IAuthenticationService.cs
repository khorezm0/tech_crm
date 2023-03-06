using Authentication.Models.Authentication;

namespace Authentication.Core;

public interface IAuthenticationService
{
     Task<TokenResponse> CreateAccessTokenAsync(int userId, string password);
     Task<TokenResponse> RefreshTokenAsync(string refreshToken, int userId);
     void RevokeRefreshToken(string refreshToken, int userId);
}