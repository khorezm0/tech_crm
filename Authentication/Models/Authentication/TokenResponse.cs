using Authentication.Models.Tokens;

namespace Authentication.Models.Authentication;
public class TokenResponse : BaseResponse
{
    public AccessToken Token { get; set; }

    public TokenResponse(bool success, string message, AccessToken token) : base(success, message)
    {
        Token = token;
    }
}