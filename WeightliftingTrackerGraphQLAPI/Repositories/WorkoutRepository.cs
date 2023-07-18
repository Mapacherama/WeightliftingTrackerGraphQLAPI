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
        private const string SelectQuery = "SELECT * FROM Workout WHERE ExerciseName = @ExerciseName AND Sets = @Sets AND Reps = @Reps AND Weight = @Weight;";
        private const string InsertQuery = "INSERT INTO Workout (ExerciseName, Sets, Reps, Weight) VALUES (@ExerciseName, @Sets, @Reps, @Weight);";
        private const string SelectLastId = "SELECT LAST_INSERT_ID();";

        public WorkoutRepository(IMySqlDataAccess dataAccess)
        {
            _dataAccess = dataAccess ?? throw new ArgumentNullException(nameof(dataAccess));
        }

        public Workout CreateWorkout(Workout newWorkout)
        {
            if (newWorkout == null)
            {
                throw new ArgumentNullException(nameof(newWorkout));
            }

            DataTable dt = _dataAccess.ExecuteQuery(SelectQuery, CreateParameters(newWorkout));

            if (dt != null && dt.Rows.Count > 0)
            {
                throw new Exception(ErrorMessages.WorkoutDetailsExists);
            }

            
            _dataAccess.ExecuteQuery(InsertQuery, CreateParameters(newWorkout));

            newWorkout.Id = Convert.ToInt32(_dataAccess.ExecuteScalar(SelectLastId, null));

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

        private Workout WorkoutFromDataRow(DataRow row)
        {
            if (row.IsNull("Id") || row.IsNull("ExerciseName") || row.IsNull("Sets") || row.IsNull("Reps") || row.IsNull("Weight"))
            {
                throw new NullReferenceException(ErrorMessages.RowValueIsNull);
            }

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
            string query = "SELECT * FROM Workout";

            DataTable dt = _dataAccess.ExecuteQuery(query, null);

            if (dt == null)
            {
                throw new NullReferenceException(ErrorMessages.DataTableIsNull);
            }

            return dt.AsEnumerable().Select(row => WorkoutFromDataRow(row)).ToList();
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
