namespace Authentication.Models.Authentication;

public record class User
{
    public int Id { get; set; }
    public string PasswordHash { get; set; } = string.Empty;
    public string[] Roles { get; set; } = Array.Empty<string>();
}