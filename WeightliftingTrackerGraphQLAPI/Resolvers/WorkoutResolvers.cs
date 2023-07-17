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
            if (updatedWorkout == null)
            {
                throw new ArgumentNullException(nameof(updatedWorkout));
            }

            string selectQuery = "SELECT * FROM Workout WHERE Id = @Id;";
            MySqlParameter selectParameter = new MySqlParameter("@Id", updatedWorkout.Id);
            DataTable dt = _dataAccess.ExecuteQuery(selectQuery, new MySqlParameter[] { selectParameter });

            if (dt == null || dt.Rows.Count == 0)
            {
                throw new Exception($"No workout found with ID: {updatedWorkout.Id}");
            }

            string sqlQuery = "UPDATE Workout SET ExerciseName = @ExerciseName, Sets = @Sets, Reps = @Reps, Weight = @Weight WHERE Id = @Id;";

            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@Id", updatedWorkout.Id),
                new MySqlParameter("@ExerciseName", updatedWorkout.ExerciseName),
                new MySqlParameter("@Sets", updatedWorkout.Sets),
                new MySqlParameter("@Reps", updatedWorkout.Reps),
                new MySqlParameter("@Weight", updatedWorkout.Weight)
            };

            _dataAccess.ExecuteQuery(sqlQuery, parameters);

            return updatedWorkout;
        }


    }
}
