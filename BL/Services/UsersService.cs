using DAL.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace BL.Services
{
    public class UsersService
    {
        public readonly IRepository<User> Repository;

        public UsersService(IRepository<User> repository)
        {
            Repository = repository;
        }

        //Create Method
        public async Task<User> AddAsync(User user)
        {
            try
            {
                return await Repository.CreateAsync(user);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteAsync(string Id)
        {
            try
            {
                if (string.IsNullOrEmpty(Id) || Id == "0")
                    return;
                var obj = await Repository.GetByIdAsync(Id);
                if (obj != null)
                    await Repository.DeleteAsync(obj);
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
                if (string.IsNullOrEmpty(user.Id) || user.Id == "0")
                    return await AddAsync(user);

                return await Repository.UpdateAsync(user);
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
                return (await Repository.GetAllAsync()).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
