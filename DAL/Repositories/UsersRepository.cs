using DAL.Data;
using DAL.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class UsersRepository : IRepository<Entities.User>
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger _logger;

        public UsersRepository(ILogger<User> logger)
        {
            _logger = logger;
        }

        public async Task<Entities.User> CreateAsync(Entities.User appuser)
        {
            try
            {
                if (appuser != null)
                {
                    var obj = _appDbContext.Add<User>(ConvertFromEntity(appuser));
                    await _appDbContext.SaveChangesAsync();
                    return ConvertToEntity(obj.Entity);
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

        public async Task DeleteAsync(Entities.User appuser)
        {
            try
            {
                if (appuser != null)
                {
                    _appDbContext.Remove(ConvertFromEntity(appuser));
                    await _appDbContext.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Entities.User>> GetAllAsync()
        {
            try
            {
                var obj = await _appDbContext.AppUsers.ToListAsync();
                return obj.Select(ConvertToEntity);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Entities.User> GetByIdAsync(string Id)
        {
            try
            {
                if (Id != null)
                {
                    var obj = await _appDbContext.AppUsers.FirstOrDefaultAsync(x => x.Id == Id);
                    return ConvertToEntity(obj);
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

        public async Task<Entities.User> UpdateAsync(Entities.User appuser)
        {
            try
            {
                if (appuser == null)
                    return null;
                var obj = _appDbContext.Update<User>(ConvertFromEntity(appuser));
                await _appDbContext.SaveChangesAsync();
                return ConvertToEntity(obj.Entity);
            }
            catch (Exception)
            {
                throw;
            }
        }

        internal static User ConvertFromEntity(Entities.User user)
        {
            return user == null
                ? null
                : new User()
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

        internal static Entities.User ConvertToEntity(User user)
        {
            return user == null
                ? null
                : new Entities.User(
                    user.Id,
                    user.UserName,
                    user.PhoneNumber,
                    user.PhoneNumberConfirmed,
                    user.Email,
                    user.EmailConfirmed,
                    user.PasswordHash,
                    user.FirstName,
                    user.LastName,
                    user.CreatedTime,
                    user.DeletedTime
                );
        }
    }
}
