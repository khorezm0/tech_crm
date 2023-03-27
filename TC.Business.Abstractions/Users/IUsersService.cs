using TC.Business.Abstractions.Users.Models;
using TC.Common.Models;

namespace TC.Business.Abstractions.Users
{
    public interface IUsersService
    {
        Task<User> AddAsync(User user);

        Task<int> DeleteAsync(int id);

        Task<User> UpdateAsync(User user);

        Task<User> GetByIdAsync(int id);

        Task<User> GetByUserNameAsync(string userName);
        
        Task<FilterResult<User>> GetByFilterAsync(UserFilterModel filter);
    }
}