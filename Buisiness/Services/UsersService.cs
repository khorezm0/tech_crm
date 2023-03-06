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
            try
            {
                return (await _usersDal.CreateAsync(user.Map())).Map();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteAsync(int Id)
        {
            try
            {
                if (Id == 0)
                    return;

                var obj = await _usersDal.GetByIdAsync(Id);
                if (obj != null)
                    await _usersDal.DeleteAsync(obj);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<User> UpdateUser(User user)
        {
            try
            {
                if (user.Id == 0)
                    return await AddAsync(user);

                return (await _usersDal.UpdateAsync(user.Map())).Map();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            try
            {
                return (await _usersDal.GetAllAsync()).Select(item => item.Map()).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<User> GetByIdAsync(int id)
        {
            try
            {
                return (await _usersDal.GetByIdAsync(id)).Map();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}