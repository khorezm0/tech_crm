namespace TC.Api.ClientData.Auth;

public class RevokeTokenPostDto
{
    public int UserId { get; set; }
    public string Token { get; set; }
}