using Authentication.Core.Security.Hashing;
using Authentication.Models.Authentication;
using Authentication.Security.Tokens;
using Business.Services;

namespace Authentication.Core;

public class AuthenticationService : IAuthenticationService
{
    private readonly UsersService _userService;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenHandler _tokenHandler;
    
    public AuthenticationService(
        UsersService userService, 
        IPasswordHasher passwordHasher, 
        ITokenHandler tokenHandler)
    {
        _tokenHandler = tokenHandler;
        _passwordHasher = passwordHasher;
        _userService = userService;
    }

    public async Task<TokenResponse> CreateAccessTokenAsync(int userId, string password)
    {
        var user = await _userService.GetByIdAsync(userId);

        if (user == null || !_passwordHasher.PasswordMatches(password, user.PasswordHash))
        {
            return new TokenResponse(false, "Invalid credentials.", null);
        }

        var token = _tokenHandler.CreateAccessToken(user);

        return new TokenResponse(true, null, token);
    }

    public async Task<TokenResponse> RefreshTokenAsync(string refreshToken, int userId)
    {
        var token = _tokenHandler.TakeRefreshToken(refreshToken, userId);

        if (token == null)
        {
            return new TokenResponse(false, "Invalid refresh token.", null);
        }

        if (token.IsExpired())
        {
            return new TokenResponse(false, "Expired refresh token.", null);
        }

        var user = await _userService.GetByIdAsync(userId);
        if (user == null)
        {
            return new TokenResponse(false, "Invalid refresh token.", null);
        }

        var accessToken = _tokenHandler.CreateAccessToken(user);
        return new TokenResponse(true, null, accessToken);
    }

    public void RevokeRefreshToken(string refreshToken, int userId)
    {
        _tokenHandler.RevokeRefreshToken(refreshToken, userId);
    }
}