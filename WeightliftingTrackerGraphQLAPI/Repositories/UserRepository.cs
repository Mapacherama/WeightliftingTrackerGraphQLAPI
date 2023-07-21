using MySql.Data.MySqlClient;
using System.Data;
using WeightliftingTrackerGraphQLAPI.Data;
using WeightliftingTrackerGraphQLAPI.Models;
using WeightliftingTrackerGraphQLAPI.Helpers;
using AutoMapper;

namespace WeightliftingTrackerGraphQLAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMySqlDataAccess _dataAccess;
        private readonly IMapper _mapper;

        public UserRepository(IMySqlDataAccess dataAccess, IMapper mapper)
        {
            _dataAccess = dataAccess ?? throw new ArgumentNullException(nameof(dataAccess));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<User> CreateUser(User newUser)
        {
            ValidationHelper.CheckIfNull(newUser, nameof(newUser));

            DataTable dt = await _dataAccess.ExecuteQueryAsync(Queries.QuerySelectUserByUsername, CreateParameters(newUser));

            if (dt != null && dt.Rows.Count > 0)
            {
                throw new Exception(ErrorMessages.UsernameExists);
            }

            await _dataAccess.ExecuteQueryAsync(Queries.MutationInsertNewUser, CreateParameters(newUser));

            newUser.Id = Convert.ToInt32(await _dataAccess.ExecuteScalarAsync(Queries.QuerySelectLastInsertedId, null));

            return newUser;
        }

        private MySqlParameter[] CreateParameters(User user)
        {
            return new MySqlParameter[]
            {
                new MySqlParameter("@Username", user.Username),
                new MySqlParameter("@Password", user.Password)
            };
        }

    }
}
