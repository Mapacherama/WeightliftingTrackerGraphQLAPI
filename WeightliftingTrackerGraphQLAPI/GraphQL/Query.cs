using WeightliftingTrackerGraphQLAPI.Models;
using WeightliftingTrackerGraphQLAPI.Resolvers;

namespace WeightliftingTrackerGraphQLAPI.GraphQL
{
    public class Query
    {
        private readonly WorkoutResolvers _workoutResolvers;
        private readonly NutritionResolvers _nutritionResolvers;
        private readonly UserResolvers _userResolvers;

        public Query(WorkoutResolvers workoutResolvers, UserResolvers userResolvers, NutritionResolvers nutritionResolvers)
        {
            _workoutResolvers = workoutResolvers;
            _userResolvers = userResolvers;
            _nutritionResolvers = nutritionResolvers;   
        }
        public async Task<IEnumerable<WorkoutDTO>> GetWorkouts() => await _workoutResolvers.GetWorkouts();
        public async Task<IEnumerable<NutritionDTO>> GetNutritions() => await _nutritionResolvers.GetNutritions();
        public string Login(User user) => _userResolvers.Login(user);
    }
}