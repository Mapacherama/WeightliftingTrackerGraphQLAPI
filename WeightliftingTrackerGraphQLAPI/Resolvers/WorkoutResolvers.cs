using MySql.Data.MySqlClient;
using System.Data;
using WeightliftingTrackerGraphQLAPI.Data;
using WeightliftingTrackerGraphQLAPI.Models;

namespace WeightliftingTrackerGraphQLAPI.Resolvers
{
    public class WorkoutResolvers
    {
        private readonly IMySqlDataAccess _dataAccess;

        public WorkoutResolvers(IMySqlDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public IEnumerable<Workout> GetWorkouts()
        {
            string query = "SELECT * FROM Workout";
            MySqlParameter[] parameters = null; // You can add parameters if needed.

            if (_dataAccess == null)
            {
                throw new NullReferenceException("_dataAccess is null");
            }

            DataTable dt = _dataAccess.ExecuteQuery(query, parameters);

            if (dt == null)
            {
                throw new NullReferenceException("dt is null");
            }

            List<Workout> workouts = new List<Workout>();

            foreach (DataRow row in dt.Rows)
            {
                if (row.IsNull("Id") || row.IsNull("ExerciseName") || row.IsNull("Sets") || row.IsNull("Reps") || row.IsNull("Weight"))
                {
                    throw new NullReferenceException("One of the row values is null");
                }

                Workout workout = new Workout
                {
                    Id = Convert.ToInt32(row["Id"]),
                    ExerciseName = row["ExerciseName"].ToString(),
                    Sets = Convert.ToInt32(row["Sets"]),
                    Reps = Convert.ToInt32(row["Reps"]),
                    Weight = Convert.ToDecimal(row["Weight"])
                };
                workouts.Add(workout);
            }

            return workouts;
        }


        public Workout CreateWorkout(Workout newWorkout)
        {
            if (newWorkout == null)
            {
                throw new ArgumentNullException(nameof(newWorkout));
            }

            MySqlParameter[] parameters = new MySqlParameter[]
            {
        new MySqlParameter("@ExerciseName", newWorkout.ExerciseName),
        new MySqlParameter("@Sets", newWorkout.Sets),
        new MySqlParameter("@Reps", newWorkout.Reps),
        new MySqlParameter("@Weight", newWorkout.Weight)
            };

            string selectQuery = "SELECT * FROM Workout WHERE ExerciseName = @ExerciseName AND Sets = @Sets AND Reps = @Reps AND Weight = @Weight;";
            DataTable dt = _dataAccess.ExecuteQuery(selectQuery, parameters);

            if (dt != null && dt.Rows.Count > 0)
            {
                throw new Exception("A workout with the same details already exists.");
            }

            string insertQuery = "INSERT INTO Workout (ExerciseName, Sets, Reps, Weight) VALUES (@ExerciseName, @Sets, @Reps, @Weight);";
            _dataAccess.ExecuteQuery(insertQuery, parameters);

            newWorkout.Id = Convert.ToInt32(_dataAccess.ExecuteScalar("SELECT LAST_INSERT_ID();", null));

            return newWorkout;
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
