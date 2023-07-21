using WeightliftingTrackerGraphQLAPI.Models;

namespace WeightliftingTrackerGraphQLAPI.Repositories
{
    public interface IUserRepository
    {
        Task<User> CreateUser(User newUser);

        // More when needed like: ValidateUserCredentials, Update and Delete user.
    }
}
