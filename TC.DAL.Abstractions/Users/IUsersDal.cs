using TC.DAL.Abstractions.Users.Models;

namespace TC.DAL.Abstractions.Users
{
    public interface IUsersDal
    {
        Task<UserDbModel> InsertAsync(UserDbModel user);
        Task DeleteAsync(UserDbModel user);
        Task<UserDbModel> GetByIdAsync(int id);
        Task<UserDbModel> GetByUserNameAsync(string userName);
        Task<UserDbModel> UpdateAsync(UserDbModel user);
    }
}