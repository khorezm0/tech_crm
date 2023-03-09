using TC.Business.Abstractions.Users.Models;
using TC.DAL.Abstractions.Users.Models;

namespace TC.Business.Users;

public static class UsersMapper
{
    internal static UserDbModel Map(this User user)
    {
        return user == null
            ? null
            : new UserDbModel
            {
                Id = user.Id,
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
                PhoneNumber = user.PhoneNumber,
                PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                CreatedTime = user.CreatedTime,
                DeletedTime = user.DeletedTime,
                UserName = user.UserName,
                PasswordHash = user.PasswordHash,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
    }

    internal static User Map(this UserDbModel userDbModel)
    {
        return userDbModel == null
            ? null
            : new User
            {
                Id = userDbModel.Id,
                UserName = userDbModel.UserName,
                PhoneNumber = userDbModel.PhoneNumber,
                PhoneNumberConfirmed = userDbModel.PhoneNumberConfirmed,
                Email = userDbModel.Email,
                EmailConfirmed = userDbModel.EmailConfirmed,
                PasswordHash = userDbModel.PasswordHash,
                FirstName = userDbModel.FirstName,
                LastName = userDbModel.LastName,
                CreatedTime = userDbModel.CreatedTime,
                DeletedTime = userDbModel.DeletedTime
            };
    }
}