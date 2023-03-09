namespace TC.Auth.Models.Tokens;

public abstract class JsonWebToken
{
    public string Token { get; protected set; }
    public DateTime Expiration { get; protected set; }

    public JsonWebToken(string token, DateTime expiration)
    {
        if(string.IsNullOrWhiteSpace(token))
            throw new ArgumentException("Invalid token.");

        if(expiration <= new DateTime())
            throw new ArgumentException("Invalid expiration.");

        Token = token;
        Expiration = expiration;
    }

    public bool IsExpired() => DateTime.UtcNow > Expiration;
}