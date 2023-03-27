using TC.Business.Abstractions.Users.Models;

namespace TC.Api.ClientData.Users;

public static class UserMapper
{
    public static UserDto Map(this User model)
    {
        return
            model == null
                ? null
                : new UserDto
                {
                    Id = model.Id,
                    UserName = model.UserName,
                    PhoneNumber = model.PhoneNumber,
                    PhoneNumberConfirmed = model.PhoneNumberConfirmed,
                    Email = model.Email,
                    EmailConfirmed = model.EmailConfirmed,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    CreatedTime = model.CreatedTime
                };
    }
    
    public static User Map(this UserDto model, string passwordHash, string oneTimePasswordHash)
    {
        return
            model == null
                ? null
                : new User
                {
                    Id = model.Id,
                    UserName = model.UserName,
                    PhoneNumber = model.PhoneNumber,
                    PhoneNumberConfirmed = model.PhoneNumberConfirmed,
                    Email = model.Email,
                    EmailConfirmed = model.EmailConfirmed,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    CreatedTime = model.CreatedTime,
                    ModifiedTime = model.ModifiedTime,
                    PasswordHash = passwordHash
                };
    }
}