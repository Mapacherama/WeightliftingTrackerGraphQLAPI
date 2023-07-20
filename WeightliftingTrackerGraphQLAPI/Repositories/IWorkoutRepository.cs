using WeightliftingTrackerGraphQLAPI.Models;

namespace WeightliftingTrackerGraphQLAPI.Repositories
{
    public interface IWorkoutRepository
    {
        Task<IEnumerable<WorkoutDTO>> GetWorkouts();
        Task<Workout> CreateWorkout(Workout newWorkout);
        Task<Workout> UpdateWorkout(Workout updatedWorkout);
        Task<Workout> DeleteWorkout(int workoutId);
    }
}