using HotChocolate;
using WeightliftingTrackerGraphQLAPI.Models;
using WeightliftingTrackerGraphQLAPI.Resolvers;
using WeightliftingTrackerGraphQLAPI.GraphQL.Types;

namespace WeightliftingTrackerGraphQLAPI.GraphQL
{
    public class Mutation
    {
        private readonly WorkoutResolvers _workoutResolvers;

        public Mutation(WorkoutResolvers workoutResolvers)
        {
            _workoutResolvers = workoutResolvers;
        }

        public Workout CreateWorkout(WorkoutInputDTO newWorkout)
        {
            var workout = new Workout
            {
                ExerciseName = newWorkout.ExerciseName,
                Sets = newWorkout.Sets,
                Reps = newWorkout.Reps,
                Weight = newWorkout.Weight
            };

            return _workoutResolvers.CreateWorkout(workout);
        }
    }
}
