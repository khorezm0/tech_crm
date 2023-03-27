using TC.Auth.Models.Authentication;

namespace TC.Api.ClientData.Auth;

public static class AuthUserMapper
{
    public static User MapToAuthModel(this Business.Abstractions.Users.Models.User user)
    {
        return
            user == null
                ? null
                : new User
                {
                    Id = user.Id,
                    PasswordHash = user.PasswordHash,
                    Roles = user.Roles?.Select(i => i.ToString()).ToArray()
                };
    }
    
    public static Business.Abstractions.Users.Models.User Map(this RegisterPostRequest model, string passwordHash)
    {
        return
            model == null
                ? null
                : new Business.Abstractions.Users.Models.User
                {
                    UserName = model.Login,
                    PhoneNumber = model.PhoneNumber,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    CreatedTime = DateTime.Now,
                    PasswordHash = passwordHash
                };
    }
}