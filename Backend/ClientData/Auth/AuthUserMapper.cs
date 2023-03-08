using Authentication.Models.Authentication;
using Backend.ClientData.Users;

namespace Backend.ClientData.Auth;

public static class AuthUserMapper
{
    public static User Map(this Business.Abstractions.User user)
    {
        return
            user == null
                ? null
                : new User
                {
                    Id = user.Id,
                    PasswordHash = user.PasswordHash,
                    Roles = user.Roles.Select(i => i.ToString()).ToArray()
                };
    }

    public static UserDto MapToDto(this Business.Abstractions.User user)
    {
        return user == null
            ? null
            : new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                CreatedTime = user.CreatedTime,
                EmailConfirmed = user.EmailConfirmed,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                UserName = user.UserName
            };
    }
}