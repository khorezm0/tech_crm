using DAL.Contracts;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class ServiceAppUser
    {
        public readonly IRepository<User> Repository;

        public ServiceAppUser(IRepository<User> repository)
        {
            Repository = repository;
        }

        //Create Method
        public async Task<User> AddUser(User user)
        {
            try
            {
                return await Repository.Create(user);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteUser(string Id)
        {
            try
            {
                if (string.IsNullOrEmpty(Id) || Id == "0") return;
                var obj = Repository.GetAll().FirstOrDefault(x => x.Id == Id);
                if (obj != null) Repository.Delete(obj);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdateUser(string Id)
        {
            try
            {
                if (string.IsNullOrEmpty(Id) || Id == "0") return;
                var obj = Repository.GetAll().FirstOrDefault(x => x.Id == Id);
                if (obj != null)
                    Repository.Update(obj);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<User> GetAllUser()
        {
            try
            {
                return Repository.GetAll().ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}