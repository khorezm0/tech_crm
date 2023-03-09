using Microsoft.Extensions.Logging;
using TC.AspNetCore.DependencyInjection;
using TC.DAL.Abstractions.Data;
using TC.DAL.Abstractions.Users;
using TC.DAL.Abstractions.Users.Models;
using TC.DAL.Data;

namespace TC.DAL.Users
{
    [InjectAsSingleton]
    public class UsersDal : BaseDal, IUsersDal
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;
        private readonly ILogger _logger;

        public UsersDal(IDbConnectionFactory dbConnectionFactory, ILogger<UsersDal> logger)
        : base(dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _logger = logger;
        }

        public Task<UserDbModel> InsertAsync(UserDbModel user)
        {
            return 
                user != null
                    ? FirstOrDefaultAsync<UserDbModel>(Scripts.Insert, user, CancellationToken.None) 
                    : null;
        }

        public Task DeleteAsync(UserDbModel user)
        {
            if (user == null)
            {
                return Task.CompletedTask;
            }

            var queryObject = new {
                Id = user.Id
            };
            
            return FirstOrDefaultAsync<UserDbModel>(Scripts.Delete, queryObject, CancellationToken.None);
        }

        public Task<UserDbModel> GetByIdAsync(int id)
        {
            var queryObject = new {
                Id = id
            };
            
            return FirstOrDefaultAsync<UserDbModel>(Scripts.SelectById, queryObject, CancellationToken.None);
        }

        public Task<UserDbModel> GetByUserNameAsync(string userName)
        {
            var queryObject = new {
                UserName = userName
            };
            
            return FirstOrDefaultAsync<UserDbModel>(Scripts.SelectByUserName, queryObject, CancellationToken.None);
        }

        public Task<UserDbModel> UpdateAsync(UserDbModel user)
        {
            return FirstOrDefaultAsync<UserDbModel>(Scripts.Update, user, CancellationToken.None);
        }
    }
}