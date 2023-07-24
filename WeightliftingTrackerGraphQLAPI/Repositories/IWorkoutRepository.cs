using WeightliftingTrackerGraphQLAPI.Models;

namespace WeightliftingTrackerGraphQLAPI.Repositories
{
    public interface IWorkoutRepository
    {
        Task<IEnumerable<WorkoutDTO>> GetWorkouts();
        Task<Nutrition> CreateWorkout(Nutrition newWorkout);
        Task<Nutrition> UpdateWorkout(Nutrition updatedWorkout);
        Task<Nutrition> DeleteWorkout(int workoutId);
    }
}