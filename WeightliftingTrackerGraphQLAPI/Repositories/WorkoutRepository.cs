using MySql.Data.MySqlClient;
using System.Data;
using WeightliftingTrackerGraphQLAPI.Data;
using WeightliftingTrackerGraphQLAPI.Models;
using WeightliftingTrackerGraphQLAPI.Helpers;
using AutoMapper;

namespace WeightliftingTrackerGraphQLAPI.Repositories
{
    public class WorkoutRepository : IWorkoutRepository
    {
        private readonly IMySqlDataAccess _dataAccess;
        private readonly IMapper _mapper;

        public WorkoutRepository(IMySqlDataAccess dataAccess, IMapper mapper)
        {
            _dataAccess = dataAccess ?? throw new ArgumentNullException(nameof(dataAccess));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Workout> CreateWorkout(Workout newWorkout)
        {
            ValidationHelper.CheckIfNull(newWorkout, nameof(newWorkout));

            DataTable dt = await _dataAccess.ExecuteQueryAsync(Queries.QuerySelectWorkoutByDetails, CreateParameters(newWorkout));

            if (dt != null && dt.Rows.Count > 0)
            {
                throw new Exception(ErrorMessages.WorkoutDetailsExists);
            }

            
            await _dataAccess.ExecuteQueryAsync(Queries.MutationInsertNewWorkout, CreateParameters(newWorkout));

            newWorkout.Id = Convert.ToInt32(await _dataAccess.ExecuteScalarAsync(Queries.QuerySelectLastInsertedId, null));

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

        public async Task<Workout> DeleteWorkout(int workoutId)
        {
            MySqlParameter selectParameter = new MySqlParameter("@WorkoutId", workoutId);
            DataTable dt = await _dataAccess.ExecuteQueryAsync(Queries.QuerySelectWorkoutById, new MySqlParameter[] { selectParameter });

            if (dt == null || dt.Rows.Count == 0)
            {
                throw new Exception($"No workout found with ID: {workoutId}");
            }

            Workout deletedWorkout = new Workout
            {
                Id = Convert.ToInt32(dt.Rows[0]["Id"]),
                ExerciseName = dt.Rows[0]["ExerciseName"].ToString(),
                Sets = Convert.ToInt32(dt.Rows[0]["Sets"]),
                Reps = Convert.ToInt32(dt.Rows[0]["Reps"]),
                Weight = Convert.ToDecimal(dt.Rows[0]["Weight"])
            };

            
            MySqlParameter deleteParameter = new MySqlParameter("@WorkoutId", workoutId);
            await _dataAccess.ExecuteQueryAsync(Queries.MutationDeleteWorkout, new MySqlParameter[] { deleteParameter });

            return deletedWorkout;
        }

        public async Task<IEnumerable<WorkoutDTO>> GetWorkouts()
        {

            DataTable dt = await _dataAccess.ExecuteQueryAsync(Queries.QuerySelectAllWorkouts, null);

            if (dt == null)
            {
                throw new NullReferenceException(ErrorMessages.DataTableIsNull);
            }

            return dt.AsEnumerable()
            .Select(row => WorkoutFromDataRow(row))
            .Select(workout => _mapper.Map<WorkoutDTO>(workout))
            .ToList();
        }

        public async Task<Workout> UpdateWorkout(Workout updatedWorkout)
        {
            ValidationHelper.CheckIfNull(updatedWorkout, nameof(updatedWorkout));

            MySqlParameter selectParameter = new MySqlParameter("@Id", updatedWorkout.Id);
            DataTable dt = await _dataAccess.ExecuteQueryAsync(Queries.QuerySelectWorkoutById, new MySqlParameter[] { selectParameter });

            if (dt == null || dt.Rows.Count == 0)
            {
                throw new Exception($"No workout found with ID: {updatedWorkout.Id}");
            }            

            await _dataAccess.ExecuteQueryAsync(Queries.MutationUpdateExistingWorkout, UpdateParameters(updatedWorkout));

            return updatedWorkout;
        }
    }
}
