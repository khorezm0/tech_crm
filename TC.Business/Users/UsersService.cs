using TC.AspNetCore.DependencyInjection;
using TC.Business.Abstractions.Users;
using TC.Business.Abstractions.Users.Models;
using TC.Common.Models;
using TC.DAL.Abstractions.Users;

namespace TC.Business.Users
{
    public class UsersService : IUsersService
    {
        private readonly IUsersDal _usersDal;

        public UsersService(IUsersDal usersDal)
        {
            _usersDal = usersDal;
        }

        public async Task<User> AddAsync(User user)
        {
            return (await _usersDal.InsertAsync(user.Map())).Map();
        }

        public async Task<int> DeleteAsync(int id)
        {
            var obj = await _usersDal.GetByIdAsync(id);
            return await _usersDal.DeleteAsync(obj);
        }

        public async Task<User> UpdateAsync(User user)
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

        public async Task<FilterResult<User>> GetByFilterAsync(UserFilterModel model)
        {
            var res = await _usersDal.GetByFilterAsync(model.Map());
            return new FilterResult<User>(res.Data.Select(item => item.Map()).ToArray(), res.TotalCount, res.Offset,
                res.Count);
        }
    }
}