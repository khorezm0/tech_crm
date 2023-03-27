namespace TC.Api.ClientData.Auth;

public class RefreshTokenPostDto
{
    public int UserId { get; set; }
    public string Token { get; set; }
}