using System.Security.Claims;
using TC.Auth.Models.Authentication;

namespace TC.Auth.Core;

public interface IAuthenticationService
{
     TokenResponse CreateAccessToken(User user, string password);
     TokenResponse RefreshToken(User user, string refreshToken);
     void RevokeRefreshToken(User user, string refreshToken);
     int? GetClaimsUserId(ClaimsPrincipal user);
}