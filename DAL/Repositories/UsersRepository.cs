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
            try
            {
                if (appuser != null)
                {
                    var obj = _appDbContext.Add<UserDbModel>(appuser);
                    await _appDbContext.SaveChangesAsync();
                    return obj.Entity;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteAsync(UserDbModel appuser)
        {
            try
            {
                if (appuser != null)
                {
                    _appDbContext.Remove(appuser);
                    await _appDbContext.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<UserDbModel>> GetAllAsync()
        {
            try
            {
                var obj = await _appDbContext.Users.ToListAsync();
                return obj;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<UserDbModel> GetByIdAsync(int Id)
        {
            try
            {
                if (Id != null)
                {
                    var obj = await _appDbContext.Users.FirstOrDefaultAsync(x => x.Id == Id);
                    return obj;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<UserDbModel> UpdateAsync(UserDbModel appuser)
        {
            try
            {
                if (appuser == null)
                    return null;
                var obj = _appDbContext.Update<UserDbModel>(appuser);
                await _appDbContext.SaveChangesAsync();
                return obj.Entity;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}