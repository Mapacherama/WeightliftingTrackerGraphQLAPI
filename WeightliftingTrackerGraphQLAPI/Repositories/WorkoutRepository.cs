using MySql.Data.MySqlClient;
using System.Data;
using WeightliftingTrackerGraphQLAPI.Data;
using WeightliftingTrackerGraphQLAPI.Models;
using WeightliftingTrackerGraphQLAPI.Helpers;

namespace WeightliftingTrackerGraphQLAPI.Repositories
{
    public class WorkoutRepository : IWorkoutRepository
    {
        private readonly IMySqlDataAccess _dataAccess;
        private const string QuerySelectAllWorkouts = "SELECT * FROM Workout";
        private const string QuerySelectWorkoutByDetails = "SELECT * FROM Workout WHERE ExerciseName = @ExerciseName AND Sets = @Sets AND Reps = @Reps AND Weight = @Weight;";
        private const string QuerySelectWorkoutById = "SELECT * FROM Workout WHERE Id = @Id;";
        private const string MutationInsertNewWorkout = "INSERT INTO Workout (ExerciseName, Sets, Reps, Weight) VALUES (@ExerciseName, @Sets, @Reps, @Weight);";
        private const string MutationUpdateExistingWorkout = "UPDATE Workout SET ExerciseName = @ExerciseName, Sets = @Sets, Reps = @Reps, Weight = @Weight WHERE Id = @Id;";
        private const string QuerySelectLastInsertedId = "SELECT LAST_INSERT_ID();";

        public WorkoutRepository(IMySqlDataAccess dataAccess)
        {
            _dataAccess = dataAccess ?? throw new ArgumentNullException(nameof(dataAccess));
        }

        public Workout CreateWorkout(Workout newWorkout)
        {
            ValidationHelper.CheckIfNull(newWorkout, nameof(newWorkout));

            DataTable dt = _dataAccess.ExecuteQuery(QuerySelectWorkoutByDetails, CreateParameters(newWorkout));

            if (dt != null && dt.Rows.Count > 0)
            {
                throw new Exception(ErrorMessages.WorkoutDetailsExists);
            }

            
            _dataAccess.ExecuteQuery(MutationInsertNewWorkout, CreateParameters(newWorkout));

            newWorkout.Id = Convert.ToInt32(_dataAccess.ExecuteScalar(QuerySelectLastInsertedId, null));

            return newWorkout;
        }

        private MySqlParameter[] CreateParameters(Workout workout)
        {
            return new MySqlParameter[]
            {
        new MySqlParameter("@ExerciseName", workout.ExerciseName),
        new MySqlParameter("@Sets", workout.Sets),
        new MySqlParameter("@Reps", workout.Reps),
        new MySqlParameter("@Weight", workout.Weight)
            };
        }
        private MySqlParameter[] UpdateParameters(Workout workout)
        {
            return new MySqlParameter[]
            {
                new MySqlParameter("@Id", workout.Id),
                new MySqlParameter("@ExerciseName", workout.ExerciseName),
                new MySqlParameter("@Sets", workout.Sets),
                new MySqlParameter("@Reps", workout.Reps),
                new MySqlParameter("@Weight", workout.Weight)
            };
        }

        private Workout WorkoutFromDataRow(DataRow row)
        {
            DataRowHelper.CheckDataRow(row, "Id", "ExerciseName", "Sets", "Reps", "Weight");

            return new Workout
            {
                Id = Convert.ToInt32(row["Id"]),
                ExerciseName = row["ExerciseName"].ToString(),
                Sets = Convert.ToInt32(row["Sets"]),
                Reps = Convert.ToInt32(row["Reps"]),
                Weight = Convert.ToDecimal(row["Weight"])
            };
        }

        public Workout DeleteWorkout(int workoutId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Workout> GetWorkouts()
        {

            DataTable dt = _dataAccess.ExecuteQuery(QuerySelectAllWorkouts, null);

            if (dt == null)
            {
                throw new NullReferenceException(ErrorMessages.DataTableIsNull);
            }

            return dt.AsEnumerable().Select(row => WorkoutFromDataRow(row)).ToList();
        }

        public Workout UpdateWorkout(Workout updatedWorkout)
        {
            ValidationHelper.CheckIfNull(updatedWorkout, nameof(updatedWorkout));

            MySqlParameter selectParameter = new MySqlParameter("@Id", updatedWorkout.Id);
            DataTable dt = _dataAccess.ExecuteQuery(QuerySelectWorkoutById, new MySqlParameter[] { selectParameter });

            if (dt == null || dt.Rows.Count == 0)
            {
                throw new Exception($"No workout found with ID: {updatedWorkout.Id}");
            }            

            _dataAccess.ExecuteQuery(MutationUpdateExistingWorkout, UpdateParameters(updatedWorkout));

            return updatedWorkout;
        }
    }
}
