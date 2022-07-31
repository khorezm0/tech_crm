namespace Backend.Models.Responses.Users;

public class AuthorizationModel
{
    public string ApiKey { get; set; }
    public DateTime Expiration { get; set; }

    public UserModel User { get; set; }

    public string Role { get; set; }
}
