namespace TC.Auth.Models.Tokens;
public class RefreshToken : JsonWebToken
{
    public RefreshToken(string token, DateTime expiration) : base(token, expiration)
    { }
}