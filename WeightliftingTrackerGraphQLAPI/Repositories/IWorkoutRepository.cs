using WeightliftingTrackerGraphQLAPI.Models;

namespace WeightliftingTrackerGraphQLAPI.Repositories
{
    public interface IWorkoutRepository
    {
        IEnumerable<Workout> GetWorkouts();
        Workout CreateWorkout(Workout newWorkout);
        Workout UpdateWorkout(Workout updatedWorkout);
        Workout DeleteWorkout(int workoutId);
    }
}