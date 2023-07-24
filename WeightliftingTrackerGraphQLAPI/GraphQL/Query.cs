using WeightliftingTrackerGraphQLAPI.Models;
using WeightliftingTrackerGraphQLAPI.Resolvers;

namespace WeightliftingTrackerGraphQLAPI.GraphQL
{
    public class Query
    {
        private readonly WorkoutResolvers _workoutResolvers;
        private readonly UserResolvers _userResolvers;

        public Query(WorkoutResolvers workoutResolvers, UserResolvers userResolvers)
        {
            _workoutResolvers = workoutResolvers;
            _userResolvers = userResolvers;
        }
        public async Task<IEnumerable<WorkoutDTO>> GetWorkouts() => await _workoutResolvers.GetWorkouts();
        public string Login(User user) => _userResolvers.Login(user);
    }
}