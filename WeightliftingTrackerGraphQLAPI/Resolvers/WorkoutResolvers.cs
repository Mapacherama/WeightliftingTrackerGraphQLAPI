using MySql.Data.MySqlClient;
using System.Data;
using WeightliftingTrackerGraphQLAPI.Data;
using WeightliftingTrackerGraphQLAPI.Models;
using WeightliftingTrackerGraphQLAPI.Repositories;

namespace WeightliftingTrackerGraphQLAPI.Resolvers
{
    public class WorkoutResolvers
    {
        private readonly IWorkoutRepository _workoutRepository;

        public WorkoutResolvers(IWorkoutRepository workoutRepository)
        {
            _workoutRepository = workoutRepository;
        }

        public async Task<IEnumerable<WorkoutDTO>> GetWorkouts()
        {
            return await _workoutRepository.GetWorkouts();
        }


        public async Task<Nutrition> CreateWorkout(Nutrition newWorkout)
        {
            return await _workoutRepository.CreateWorkout(newWorkout);
        }

        public async Task<Nutrition> DeleteWorkout(int id)
        {
            return await _workoutRepository.DeleteWorkout(id);
        }


        public async Task<Nutrition> UpdateWorkout(Nutrition updatedWorkout)
        {
            return await _workoutRepository.UpdateWorkout(updatedWorkout);
        }


    }
}
