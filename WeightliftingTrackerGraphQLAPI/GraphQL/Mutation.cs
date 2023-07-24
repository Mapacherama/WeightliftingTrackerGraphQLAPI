using HotChocolate;
using WeightliftingTrackerGraphQLAPI.Models;
using WeightliftingTrackerGraphQLAPI.Resolvers;
using WeightliftingTrackerGraphQLAPI.GraphQL.Types;
using WeightliftingTrackerGraphQLAPI.Repositories;

namespace WeightliftingTrackerGraphQLAPI.GraphQL
{
    public class Mutation
    {
        private readonly WorkoutResolvers _workoutResolvers;
        private readonly UserResolvers _userResolvers;
        public Mutation(WorkoutResolvers workoutResolvers, UserResolvers userResolvers)
        {
            _workoutResolvers = workoutResolvers;
            _userResolvers = userResolvers;
        }

        public async Task<Nutrition> CreateWorkout(WorkoutInputDTO newWorkout)
        {
            var workout = new Nutrition
            {
                ExerciseName = newWorkout.ExerciseName,
                Sets = newWorkout.Sets,
                Reps = newWorkout.Reps,
                Weight = newWorkout.Weight
            };

            return await _workoutResolvers.CreateWorkout(workout);
        }

        public async Task<Nutrition> DeleteWorkout(int id)
        {
            return await _workoutResolvers.DeleteWorkout(id);
        }

        public async Task<Nutrition> UpdateWorkout(WorkoutUpdateInputDTO updatedWorkoutDto)
        {
            var workout = new Nutrition
            {
                Id = updatedWorkoutDto.Id,
                ExerciseName = updatedWorkoutDto.ExerciseName,
                Sets = updatedWorkoutDto.Sets,
                Reps = updatedWorkoutDto.Reps,
                Weight = updatedWorkoutDto.Weight
            };

            return await _workoutResolvers.UpdateWorkout(workout);
        }

        public async Task<User> CreateUser(UserInputDTO newUser)
        {
            var user = new User
            {
                Username = newUser.Username,
                Password = newUser.Password,
            };

            return await _userResolvers.CreateUser(user);
        }
    }
}
