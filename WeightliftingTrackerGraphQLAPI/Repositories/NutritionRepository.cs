using AutoMapper;
using MySql.Data.MySqlClient;
using System.Data;
using WeightliftingTrackerGraphQLAPI.Data;
using WeightliftingTrackerGraphQLAPI.Helpers;
using WeightliftingTrackerGraphQLAPI.Models;

namespace WeightliftingTrackerGraphQLAPI.Repositories
{
    public class NutritionRepository : INutritionRepository
    {
        private readonly IMySqlDataAccess _dataAccess;
        private readonly IMapper _mapper;

        public NutritionRepository(IMySqlDataAccess dataAccess, IMapper mapper)
        {
            _dataAccess = dataAccess;
            _mapper = mapper;
        }

        public async Task<Nutrition> CreateNutrition(Nutrition newNutrition)
        {
            ValidationHelper.CheckIfNull(newNutrition, nameof(newNutrition));

            DataTable dt = await _dataAccess.ExecuteQueryAsync(Queries.QuerySelectNutritionByDetails, CreateParameters(newNutrition));

            if (dt != null && dt.Rows.Count > 0)
            {
                throw new Exception(ErrorMessages.NutritionDetailsExists);
            }

            await _dataAccess.ExecuteQueryAsync(Queries.MutationInsertNewNutrition, CreateParameters(newNutrition));

            newNutrition.Id = Convert.ToInt32(await _dataAccess.ExecuteScalarAsync(Queries.QuerySelectLastInsertedId, null));

            return newNutrition;
        }

        private MySqlParameter[] CreateParameters(Nutrition nutrition)
        {
            return new MySqlParameter[]
            {
                new MySqlParameter("@Calories", nutrition.Calories),
                new MySqlParameter("@Protein", nutrition.Protein),
                new MySqlParameter("@Carbohydrates", nutrition.Carbohydrates),
                new MySqlParameter("@Fats", nutrition.Fats)
            };
        }

        private MySqlParameter[] UpdateParameters(Nutrition nutrition)
        {
            return new MySqlParameter[]
            {
                new MySqlParameter("@Id", nutrition.Id),
                new MySqlParameter("@Calories", nutrition.Calories),
                new MySqlParameter("@Protein", nutrition.Protein),
                new MySqlParameter("@Carbohydrates", nutrition.Carbohydrates),
                new MySqlParameter("@Fats", nutrition.Fats)
            };
        }

        private Nutrition NutritionFromDataRow(DataRow row)
        {
            DataRowHelper.CheckDataRow(row, "Id", "Calories", "Protein", "Carbohydrates", "Fats");

            return new Nutrition
            {
                Id = Convert.ToInt32(row["Id"]),
                Calories = Convert.ToSingle(row["Calories"]),
                Protein = Convert.ToSingle(row["Protein"]),
                Carbohydrates = Convert.ToSingle(row["Carbohydrates"]),
                Fats = Convert.ToSingle(row["Fats"])
            };
        }

        public async Task<Nutrition> DeleteNutrition(int nutritionId)
        {
            MySqlParameter selectParameter = new MySqlParameter("@NutritionId", nutritionId);
            DataTable dt = await _dataAccess.ExecuteQueryAsync(Queries.QuerySelectNutritionById, new MySqlParameter[] { selectParameter });

            if (dt == null || dt.Rows.Count == 0)
            {
                throw new Exception($"No nutrition found with ID: {nutritionId}");
            }

            Nutrition deletedNutrition = new Nutrition
            {
                Id = Convert.ToInt32(dt.Rows[0]["Id"]),
                Calories = Convert.ToSingle(dt.Rows[0]["Calories"]),
                Protein = Convert.ToSingle(dt.Rows[0]["Protein"]),
                Carbohydrates = Convert.ToSingle(dt.Rows[0]["Carbohydrates"]),
                Fats = Convert.ToSingle(dt.Rows[0]["Fats"])
            };

            MySqlParameter deleteParameter = new MySqlParameter("@NutritionId", nutritionId);
            await _dataAccess.ExecuteQueryAsync(Queries.MutationDeleteNutrition, new MySqlParameter[] { deleteParameter });

            return deletedNutrition;
        }

        public async Task<IEnumerable<NutritionDTO>> GetNutritions()
        {
            DataTable dt = await _dataAccess.ExecuteQueryAsync(Queries.QuerySelectAllNutritions, null);

            if (dt == null)
            {
                throw new NullReferenceException(ErrorMessages.DataTableIsNull);
            }

            return dt.AsEnumerable()
            .Select(row => NutritionFromDataRow(row))
            .Select(nutrition => _mapper.Map<NutritionDTO>(nutrition))
            .ToList();
        }

        public async Task<Nutrition> UpdateNutrition(Nutrition updatedNutrition)
        {
            ValidationHelper.CheckIfNull(updatedNutrition, nameof(updatedNutrition));

            MySqlParameter selectParameter = new MySqlParameter("@Id", updatedNutrition.Id);
            DataTable dt = await _dataAccess.ExecuteQueryAsync(Queries.QuerySelectNutritionById, new MySqlParameter[] { selectParameter });

            if (dt == null || dt.Rows.Count == 0)
            {
                throw new Exception($"No nutrition found with ID: {updatedNutrition.Id}");
            }

            await _dataAccess.ExecuteQueryAsync(Queries.MutationUpdateExistingNutrition, UpdateParameters(updatedNutrition));

            return updatedNutrition;
        }
    }
}
