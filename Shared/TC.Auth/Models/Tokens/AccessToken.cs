namespace TC.Auth.Models.Tokens;
public class AccessToken : JsonWebToken
{
    public RefreshToken RefreshToken { get; private set; }

    public AccessToken(string token, DateTime expiration, RefreshToken refreshToken) : base(token, expiration)
    {
        RefreshToken = refreshToken 
            ?? throw new ArgumentException("Specify a valid refresh token.");
    }
}