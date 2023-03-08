using System.Security.Claims;
using Authentication.Models.Authentication;

namespace Authentication.Core;

public interface IAuthenticationService
{
     TokenResponse CreateAccessTokenAsync(User user, string password);
     TokenResponse RefreshTokenAsync(User user, string refreshToken);
     void RevokeRefreshToken(User user, string refreshToken);
     int GetClaimsUserId(ClaimsPrincipal user);
}