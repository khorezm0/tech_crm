using DAL.Data;
using DAL.Models;
using Microsoft.Extensions.Logging;
using DAL.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class UsersRepository : IRepository<UserDbModel>
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger _logger;

        public UsersRepository(ILogger logger)
        {
            _logger = logger;
        }

        public async Task<UserDbModel> CreateAsync(UserDbModel appuser)
        {
            if (appuser != null)
            {
                var obj = _appDbContext.Add<UserDbModel>(appuser);
                await _appDbContext.SaveChangesAsync();
                return obj.Entity;
            }

            return null;
        }

        public async Task DeleteAsync(UserDbModel appuser)
        {
            if (appuser != null)
            {
                _appDbContext.Remove(appuser);
                await _appDbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<UserDbModel>> GetAllAsync()
        {
            var obj = await _appDbContext.Users.ToListAsync();
            return obj;
        }

        public async Task<UserDbModel> GetByIdAsync(int id)
        {
            var obj = await _appDbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            return obj;
        }

        public async Task<UserDbModel> GetByUserNameAsync(string userName)
        {
            if (!string.IsNullOrWhiteSpace(userName))
            {
                var obj = await _appDbContext.Users.FirstOrDefaultAsync(x =>
                    x.UserName == userName || x.PhoneNumber == userName || x.Email == userName);
                return obj;
            }

            return null;
        }

        public async Task<UserDbModel> UpdateAsync(UserDbModel appuser)
        {
            if (appuser == null)
                return null;
            var obj = _appDbContext.Update<UserDbModel>(appuser);
            await _appDbContext.SaveChangesAsync();
            return obj.Entity;
        }
    }
}