using Microsoft.Extensions.Logging;
using TC.Common.Models;
using TC.DAL.Abstractions.Data;
using TC.DAL.Abstractions.Users;
using TC.DAL.Abstractions.Users.Models;
using TC.DAL.Data;

namespace TC.DAL.Users
{
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
            var queryObject = new
            {
                user.UserName,
                user.Email,
                user.EmailConfirmed,
                user.FirstName,
                user.LastName,
                user.PasswordHash,
                user.PhoneNumber,
                user.PhoneNumberConfirmed,
                CreatedTime = DateTime.Now
            };
            return FirstOrDefaultAsync<UserDbModel>(Scripts.Insert, queryObject, CancellationToken.None);
        }

        public Task<int> DeleteAsync(UserDbModel user)
        {
            var queryObject = new {
                user.Id
            };
            
            return FirstOrDefaultAsync<int>(Scripts.Delete, queryObject, CancellationToken.None);
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
            var queryObject = new
            {
                user.Id,
                user.UserName,
                user.Email,
                user.EmailConfirmed,
                user.FirstName,
                user.LastName,
                user.PasswordHash,
                user.PhoneNumber,
                user.PhoneNumberConfirmed,
                ModifiedTime = DateTime.Now,
            };
            return FirstOrDefaultAsync<UserDbModel>(Scripts.Update, queryObject, CancellationToken.None, true);
        }

        public async Task<FilterResult<UserDbModel>> GetByFilterAsync(UserDbFilterModel model)
        {
            var queryObject = new
            {
                model.UserIds,
                model.Limit,
                model.Offset,
            };

            var objects = QueryAsync<UserDbModel>(Scripts.SelectByFilter, queryObject, CancellationToken.None, true);
            var totalCount = FirstOrDefaultAsync<long>(Scripts.CountByFilter, queryObject, CancellationToken.None, true);
            await Task.WhenAll(objects, totalCount);
            return new FilterResult<UserDbModel>(objects.Result, totalCount.Result, model.Offset, objects.Result.Count);
        }
    }
}