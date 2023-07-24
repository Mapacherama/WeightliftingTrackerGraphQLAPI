using HotChocolate;
using WeightliftingTrackerGraphQLAPI.Models;
using WeightliftingTrackerGraphQLAPI.Resolvers;
using WeightliftingTrackerGraphQLAPI.GraphQL.Types;

namespace WeightliftingTrackerGraphQLAPI.GraphQL
{
    public class Mutation
    {
        private readonly WorkoutResolvers _workoutResolvers;
        private readonly UserResolvers _userResolvers;
        private readonly NutritionResolvers _nutritionResolvers;
        public Mutation(WorkoutResolvers workoutResolvers, UserResolvers userResolvers, NutritionResolvers nutritionResolvers)
        {
            _workoutResolvers = workoutResolvers;
            _userResolvers = userResolvers;
            _nutritionResolvers = nutritionResolvers;
        }

        public async Task<Workout> CreateWorkout(WorkoutInputDTO newWorkout)
        {
            var workout = new Workout
            {
                ExerciseName = newWorkout.ExerciseName,
                Sets = newWorkout.Sets,
                Reps = newWorkout.Reps,
                Weight = newWorkout.Weight
            };

            return await _workoutResolvers.CreateWorkout(workout);
        }

        public async Task<Nutrition> CreateNutrition(NutritionInputDTO newNutrition)
        {
            var nutrition = new Nutrition
            {
                FoodName = newNutrition.FoodName,
                Calories = newNutrition.Calories,
                Protein = newNutrition.Protein,
                Carbohydrates = newNutrition.Carbohydrates,
                Fats = newNutrition.Fats
            };

            return await _nutritionResolvers.CreateNutrition(nutrition); // Assuming _nutritionResolvers is the service handling the creation of Nutrition records.
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

        public async Task<Workout> DeleteWorkout(int id)
        {
            return await _workoutResolvers.DeleteWorkout(id);
        }

        public async Task<Nutrition> DeleteNutrition(int id)
        {
            return await _nutritionResolvers.DeleteNutrition(id);
        }

        public async Task<Workout> UpdateWorkout(WorkoutUpdateInputDTO updatedWorkoutDto)
        {
            var workout = new Workout
            {
                Id = updatedWorkoutDto.Id,
                ExerciseName = updatedWorkoutDto.ExerciseName,
                Sets = updatedWorkoutDto.Sets,
                Reps = updatedWorkoutDto.Reps,
                Weight = updatedWorkoutDto.Weight
            };

            return await _workoutResolvers.UpdateWorkout(workout);
        }

        public async Task<Nutrition> UpdateNutrition(NutritionUpdateInputDTO updatedNutritionDto)
        {
            var nutrition = new Nutrition
            {
                Id = updatedNutritionDto.Id,
                FoodName = updatedNutritionDto.FoodName,
                Calories = updatedNutritionDto.Calories,
                Protein = updatedNutritionDto.Protein,
                Carbohydrates = updatedNutritionDto.Carbohydrates,
                Fats = updatedNutritionDto.Fats
            };

            return await _nutritionResolvers.UpdateNutrition(nutrition);
        }

    }
}
