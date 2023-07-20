using System.Collections.Generic;
using WeightliftingTrackerGraphQLAPI.Models;
using WeightliftingTrackerGraphQLAPI.Resolvers;

namespace WeightliftingTrackerGraphQLAPI.GraphQL
{
    public class Query
    {
        private readonly WorkoutResolvers _workoutResolvers;

        public Query(WorkoutResolvers workoutResolvers)
        {
            _workoutResolvers = workoutResolvers;
        }
        public async Task<IEnumerable<WorkoutDTO>> GetWorkouts() => await _workoutResolvers.GetWorkouts();
    }
}