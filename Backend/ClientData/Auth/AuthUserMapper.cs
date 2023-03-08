using Authentication.Models.Authentication;

namespace Backend.ClientData.Auth;

public static class AuthUserMapper
{
    public static User Map(this Business.Abstractions.User user)
    {
        return new User
        {
            Id = user.Id,
            PasswordHash = user.PasswordHash,
            Roles = user.Roles.Select(i => i.ToString()).ToArray()
        };
    }
}