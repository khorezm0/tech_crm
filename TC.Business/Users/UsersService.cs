using TC.AspNetCore.DependencyInjection;
using TC.Business.Abstractions.Users;
using TC.Business.Abstractions.Users.Models;
using TC.DAL.Abstractions.Users;

namespace TC.Business.Users
{
    [InjectAsSingleton]
    public class UsersService : IUsersService
    {
        private readonly IUsersDal _usersDal;

        public UsersService(IUsersDal usersDal)
        {
            _usersDal = usersDal;
        }

        //Create Method
        public async Task<User> AddAsync(User user)
        {
            return (await _usersDal.InsertAsync(user.Map())).Map();
        }

        public async Task DeleteAsync(int id)
        {
            if (id == 0)
                return;

            var obj = await _usersDal.GetByIdAsync(id);
            if (obj != null)
                await _usersDal.DeleteAsync(obj);
        }

        public async Task<User> UpdateUser(User user)
        {
            if (user.Id == 0)
                return await AddAsync(user);

            return (await _usersDal.UpdateAsync(user.Map())).Map();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return (await _usersDal.GetByIdAsync(id)).Map();
        }

        public async Task<User> GetByUserNameAsync(string userName)
        {
            return (await _usersDal.GetByUserNameAsync(userName)).Map();
        }
    }
}