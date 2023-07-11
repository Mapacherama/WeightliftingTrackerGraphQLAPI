using System.Collections.Generic;
using System.Linq;
using WeightliftingTrackerGraphQLAPI.Models;

namespace WeightliftingTrackerGraphQLAPI.Resolvers
{
    public class WorkoutResolvers
    {
        private readonly List<Workout> _workouts; // Assume this is your data source

        public WorkoutResolvers()
        {
            _workouts = new List<Workout>(); // Initialize or fetch the workouts from your data source
        }

        public IEnumerable<Workout> GetWorkouts()
        {
            return _workouts;
        }
    }
}
