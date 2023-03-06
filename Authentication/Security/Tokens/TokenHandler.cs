using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using Microsoft.Extensions.Options;

using Authentication.Core.Security.Hashing;
using Authentication.Models.Tokens;
using Business.Abstractions;

namespace Authentication.Security.Tokens;

public class TokenHandler : ITokenHandler
{
    private readonly ISet<RefreshTokenWithEmail> _refreshTokens = new HashSet<RefreshTokenWithEmail>();

    private readonly TokenOptions _tokenOptions;
    private readonly SigningConfigurations _signingConfigurations;
    private readonly IPasswordHasher _passwordHasher;

    public TokenHandler(
        IOptions<TokenOptions> tokenOptionsSnapshot,
        SigningConfigurations signingConfigurations,
        IPasswordHasher passwordHasher)
    {
        _passwordHasher = passwordHasher;
        _tokenOptions = tokenOptionsSnapshot.Value;
        _signingConfigurations = signingConfigurations;
    }

    public AccessToken CreateAccessToken(User user)
    {
        var refreshToken = BuildRefreshToken();
        var accessToken = BuildAccessToken(user, refreshToken);

        _refreshTokens.Add(new RefreshTokenWithEmail
        {
            UserId = user.Id,
            RefreshToken = refreshToken,
        });

        return accessToken;
    }

    public RefreshToken TakeRefreshToken(string token, int userId)
    {
        if (userId <= 0)
        {
            return null;
        }

        var refreshTokenWithEmail =
            _refreshTokens.SingleOrDefault(t => t.RefreshToken.Token == token && t.UserId == userId);

        if (refreshTokenWithEmail == null)
        {
            return null;
        }

        _refreshTokens.Remove(refreshTokenWithEmail);

        return refreshTokenWithEmail.RefreshToken;
    }

    public void RevokeRefreshToken(string token, int userId)
    {
        TakeRefreshToken(token, userId);
    }

    private RefreshToken BuildRefreshToken()
    {
        var refreshToken = new RefreshToken
        (
            token: _passwordHasher.HashPassword(Guid.NewGuid().ToString()),
            expiration: DateTime.UtcNow.AddSeconds(_tokenOptions.RefreshTokenExpiration).Ticks
        );

        return refreshToken;
    }

    private AccessToken BuildAccessToken(User user, RefreshToken refreshToken)
    {
        var accessTokenExpiration = DateTime.UtcNow.AddSeconds(_tokenOptions.AccessTokenExpiration);

        var securityToken = new JwtSecurityToken
        (
            issuer: _tokenOptions.Issuer,
            audience: _tokenOptions.Audience,
            claims: GetClaims(user),
            expires: accessTokenExpiration,
            notBefore: DateTime.UtcNow,
            signingCredentials: _signingConfigurations.SigningCredentials
        );

        var handler = new JwtSecurityTokenHandler();
        var accessToken = handler.WriteToken(securityToken);

        return new AccessToken(accessToken, accessTokenExpiration.Ticks, refreshToken);
    }

    private IEnumerable<Claim> GetClaims(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
        };

        if (user.Roles != null)
        {
            claims.AddRange(user.Roles.Select(userRole => new Claim(ClaimTypes.Role, userRole.ToString())));
        }

        return claims;
    }
}