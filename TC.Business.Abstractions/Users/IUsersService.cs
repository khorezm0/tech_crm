using TC.Business.Abstractions.Users.Models;

namespace TC.Business.Abstractions.Users
{
    public interface IUsersService
    {
        Task<User> AddAsync(User user);

        Task DeleteAsync(int id);

        Task<User> UpdateUser(User user);

        Task<User> GetByIdAsync(int id);

        Task<User> GetByUserNameAsync(string userName);
    }
}