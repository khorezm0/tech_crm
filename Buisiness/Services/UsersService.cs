using Business.Abstractions;
using DAL.Repositories;

namespace Business.Services
{
    public class UsersService
    {
        private readonly UsersRepository _usersDal;

        public UsersService(UsersRepository usersDal)
        {
            _usersDal = usersDal;
        }

        //Create Method
        public async Task<User> AddAsync(User user)
        {
            return (await _usersDal.CreateAsync(user.Map())).Map();
        }

        public async Task DeleteAsync(int Id)
        {
            if (Id == 0)
                return;

            var obj = await _usersDal.GetByIdAsync(Id);
            if (obj != null)
                await _usersDal.DeleteAsync(obj);
        }

        public async Task<User> UpdateUser(User user)
        {
            if (user.Id == 0)
                return await AddAsync(user);

            return (await _usersDal.UpdateAsync(user.Map())).Map();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return (await _usersDal.GetAllAsync()).Select(item => item.Map()).ToList();
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