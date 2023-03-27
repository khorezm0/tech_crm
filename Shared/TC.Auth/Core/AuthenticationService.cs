using System.Security.Claims;
using TC.Auth.Models.Authentication;
using TC.Auth.Security.Hashing;
using TC.Auth.Security.Tokens;

namespace TC.Auth.Core;

public class AuthenticationService : IAuthenticationService
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenHandler _tokenHandler;
    
    public AuthenticationService(
        IPasswordHasher passwordHasher, 
        ITokenHandler tokenHandler)
    {
        _tokenHandler = tokenHandler;
        _passwordHasher = passwordHasher;
    }

    public TokenResponse CreateAccessToken(User user, string password)
    {
        if (user.Id <= 0 || !_passwordHasher.PasswordMatches(password, user.PasswordHash))
        {
            return new TokenResponse(false, "Invalid credentials.", null);
        }

        var token = _tokenHandler.CreateAccessToken(user);
        return new TokenResponse(true, null, token);
    }

    public TokenResponse RefreshToken(User user, string refreshToken)
    {
        var token = _tokenHandler.TakeRefreshToken(user, refreshToken);
        if (token == null)
        {
            return new TokenResponse(false, "Invalid refresh token.", null);
        }

        if (token.IsExpired())
        {
            return new TokenResponse(false, "Expired refresh token.", null);
        }

        if (user.Id <= 0)
        {
            return new TokenResponse(false, "Invalid refresh token.", null);
        }

        var accessToken = _tokenHandler.CreateAccessToken(user);
        return new TokenResponse(true, null, accessToken);
    }

    public void RevokeRefreshToken(User user, string refreshToken)
    {
        _tokenHandler.RevokeRefreshToken(user, refreshToken);
    }

    public int? GetClaimsUserId(ClaimsPrincipal user)
    {
        var claim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);
        return claim == null
            ? null
            : int.Parse(claim.Value);
    }
}