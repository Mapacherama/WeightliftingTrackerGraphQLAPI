using AutoMapper;
using MySql.Data.MySqlClient;
using System.Data;
using WeightliftingTrackerGraphQLAPI.Data;
using WeightliftingTrackerGraphQLAPI.Helpers;
using WeightliftingTrackerGraphQLAPI.Models;

namespace WeightliftingTrackerGraphQLAPI.Repositories
{
    public class HydrationRepository : IHydrationRepository
    {
        private readonly IMySqlDataAccess _dataAccess;
        private readonly IMapper _mapper;

        public HydrationRepository(IMySqlDataAccess dataAccess, IMapper mapper)
        {
            _dataAccess = dataAccess;
            _mapper = mapper;
        }

        public async Task<Hydration> CreateHydration(Hydration newHydration)
        {
            ValidationHelper.CheckIfNull(newHydration, nameof(newHydration));

            DataTable dt = await _dataAccess.ExecuteQueryAsync(Queries.QuerySelectHydrationByDetails, CreateParameters(newHydration));

            if (dt != null && dt.Rows.Count > 0)
            {
                throw new Exception(ErrorMessages.HydrationDetailsExists);
            }

            await _dataAccess.ExecuteQueryAsync(Queries.MutationInsertNewHydration, CreateParameters(newHydration));

            newHydration.Id = Convert.ToInt32(await _dataAccess.ExecuteScalarAsync(Queries.QuerySelectLastInsertedId, null));

            return newHydration;
        }

        private MySqlParameter[] CreateParameters(Hydration hydration)
        {
            return new MySqlParameter[]
            {
                new MySqlParameter("@UserId", hydration.UserId),
                new MySqlParameter("@waterIntake", hydration.waterIntake),
                new MySqlParameter("@HydrationGoal", hydration.HydrationGoal)
            };
        }

        private Hydration HydrationFromDataRow(DataRow row)
        {
            DataRowHelper.CheckDataRow(row, "Id", "UserId", "waterIntake", "HydrationGoal");

            return new Hydration
            {
                Id = Convert.ToInt32(row["Id"]),
                UserId = Convert.ToInt32(row["UserId"]),
                waterIntake = Convert.ToSingle(row["waterIntake"]),
                HydrationGoal = Convert.ToSingle(row["HydrationGoal"])
            };
        }

        public async Task<Hydration> DeleteHydration(int hydrationId)
        {
            MySqlParameter selectParameter = new MySqlParameter("@Id", hydrationId);
            DataTable dt = await _dataAccess.ExecuteQueryAsync(Queries.QuerySelectHydrationById, new MySqlParameter[] { selectParameter });

            if (dt == null || dt.Rows.Count == 0)
            {
                throw new Exception($"No hydration found with ID: {hydrationId}");
            }

            Hydration deletedHydration = HydrationFromDataRow(dt.Rows[0]);

            MySqlParameter deleteParameter = new MySqlParameter("@HydrationId", hydrationId);
            await _dataAccess.ExecuteQueryAsync(Queries.MutationDeleteHydration, new MySqlParameter[] { deleteParameter });

            return deletedHydration;
        }

        public async Task<IEnumerable<Hydration>> GetHydrations()
        {
            DataTable dt = await _dataAccess.ExecuteQueryAsync(Queries.QuerySelectAllHydrations, null);

            if (dt == null)
            {
                throw new NullReferenceException(ErrorMessages.DataTableIsNull);
            }

            return dt.AsEnumerable()
                .Select(row => HydrationFromDataRow(row))
                .ToList();
        }

        public async Task<Hydration> GetHydrationById(int id)
        {
            MySqlParameter selectParameter = new MySqlParameter("@Id", id);
            DataTable dt = await _dataAccess.ExecuteQueryAsync(Queries.QuerySelectHydrationById, new MySqlParameter[] { selectParameter });

            if (dt == null || dt.Rows.Count == 0)
            {
                throw new Exception($"No hydration found with ID: {id}");
            }

            return HydrationFromDataRow(dt.Rows[0]);
        }

        public async Task<Hydration> UpdateHydration(Hydration updatedHydration)
        {
            ValidationHelper.CheckIfNull(updatedHydration, nameof(updatedHydration));

            MySqlParameter selectParameter = new MySqlParameter("@Id", updatedHydration.Id);
            DataTable dt = await _dataAccess.ExecuteQueryAsync(Queries.QuerySelectHydrationById, new MySqlParameter[] { selectParameter });

            if (dt == null || dt.Rows.Count == 0)
            {
                throw new Exception($"No hydration found with ID: {updatedHydration.Id}");
            }

            string updateQuery = "UPDATE Hydration SET ";
            List<MySqlParameter> updateParameters = new List<MySqlParameter>();

            if (updatedHydration.waterIntake != null)
            {
                updateQuery += "waterIntake = @waterIntake, ";
                updateParameters.Add(new MySqlParameter("@waterIntake", updatedHydration.waterIntake));
            }
            if (updatedHydration.HydrationGoal != null)
            {
                updateQuery += "HydrationGoal = @HydrationGoal, ";
                updateParameters.Add(new MySqlParameter("@HydrationGoal", updatedHydration.HydrationGoal));
            }

            updateQuery = updateQuery.TrimEnd(',', ' ') + " WHERE Id = @Id;";
            updateParameters.Add(new MySqlParameter("@Id", updatedHydration.Id));

            await _dataAccess.ExecuteQueryAsync(updateQuery, updateParameters.ToArray());

            return updatedHydration;
        }
    }
}
