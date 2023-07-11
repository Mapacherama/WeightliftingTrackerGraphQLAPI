using System.Collections.Generic;
using System.Linq;
using WeightliftingTrackerGraphQLAPI.Models;
using WeightliftingTrackerGraphQLAPI.Data;
using System.Data;

namespace WeightliftingTrackerGraphQLAPI.Resolvers
{
    public class WorkoutResolvers
    {
        private readonly MySqlDataAccess _dataAccess;

        public WorkoutResolvers(MySqlDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public IEnumerable<Workout> GetWorkouts()
        {
            DataTable dt = _dataAccess.ExecuteQuery("SELECT * FROM Workouts");

            List<Workout> workouts = new List<Workout>();

            foreach (DataRow row in dt.Rows)
            {
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
    }
}
