﻿using MySql.Data.MySqlClient;
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
            string sqlQuery = "INSERT INTO Workouts (ExerciseName, Sets, Reps, Weight) VALUES (@ExerciseName, @Sets, @Reps, @Weight);";

            MySqlParameter[] parameters = new MySqlParameter[]
        {
            new MySqlParameter("@ExerciseName", newWorkout.ExerciseName),
            new MySqlParameter("@Sets", newWorkout.Sets),
            new MySqlParameter("@Reps", newWorkout.Reps),
            new MySqlParameter("@Weight", newWorkout.Weight)
        };

            _dataAccess.ExecuteQuery(sqlQuery, parameters);

            // Assuming MySqlDataAccess.ExecuteQuery returns the ID of the inserted record.
            newWorkout.Id = Convert.ToInt32(_dataAccess.ExecuteScalar(sqlQuery, parameters));

            return newWorkout;
        }

    }
}
