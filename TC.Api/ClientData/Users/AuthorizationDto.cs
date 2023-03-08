namespace TC.Api.ClientData.Users;

public class AuthorizationDto
{
    public string ApiKey { get; set; }
    public DateTime Expiration { get; set; }

    public UserDto User { get; set; }

    public string Role { get; set; }
}
