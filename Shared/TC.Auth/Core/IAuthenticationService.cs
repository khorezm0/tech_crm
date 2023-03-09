using System.Security.Claims;
using TC.Auth.Models.Authentication;

namespace TC.Auth.Core;

public interface IAuthenticationService
{
     TokenResponse CreateAccessTokenAsync(User user, string password);
     TokenResponse RefreshTokenAsync(User user, string refreshToken);
     void RevokeRefreshToken(User user, string refreshToken);
     int? GetClaimsUserId(ClaimsPrincipal user);
}