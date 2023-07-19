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

        public IEnumerable<Workout> GetWorkouts()
        {
            return _workoutRepository.GetWorkouts();
        }


        public Workout CreateWorkout(Workout newWorkout)
        {
            return _workoutRepository.CreateWorkout(newWorkout);
        }


        public Workout UpdateWorkout(Workout updatedWorkout)
        {
            return _workoutRepository.UpdateWorkout(updatedWorkout);
        }


    }
}
