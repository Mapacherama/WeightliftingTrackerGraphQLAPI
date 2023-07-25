using MySql.Data.MySqlClient;
using System.Data;
using WeightliftingTrackerGraphQLAPI.Data;
using WeightliftingTrackerGraphQLAPI.Models;
using WeightliftingTrackerGraphQLAPI.Repositories;

public class NutritionResolvers
{
    private readonly INutritionRepository _nutritionRepository;
    private readonly IMySqlDataAccess _dataAccess;

    public NutritionResolvers(INutritionRepository nutritionRepository, IMySqlDataAccess dataAccess)
    {
        _nutritionRepository = nutritionRepository;
        _dataAccess = dataAccess;
    }

    public async Task<IEnumerable<NutritionDTO>> GetNutritions()
    {
        return await _nutritionRepository.GetNutritions();
    }

    public async Task<Nutrition> CreateNutrition(Nutrition newNutrition)
    {
        return await _nutritionRepository.CreateNutrition(newNutrition);
    }

    public async Task<Nutrition> DeleteNutrition(int id)
    {
        return await _nutritionRepository.DeleteNutrition(id);
    }

    public async Task<Nutrition> UpdateNutrition(Nutrition updatedNutrition)
    {
        return await _nutritionRepository.UpdateNutrition(updatedNutrition);
    }

    public async Task<Nutrition> GetNutritionById(int id)
    {
        MySqlParameter selectParameter = new MySqlParameter("@Id", id);
        DataTable dt = await _dataAccess.ExecuteQueryAsync(Queries.QuerySelectNutritionById, new MySqlParameter[] { selectParameter });

        if (dt == null || dt.Rows.Count == 0)
        {
            throw new Exception($"No nutrition found with ID: {id}");
        }

        // Convert the first row of the DataTable to a Nutrition object
        var row = dt.Rows[0];
        var nutrition = new Nutrition
        {
            Id = Convert.ToInt32(row["Id"]),
            FoodName = row["FoodName"] as string,
            Calories = Convert.ToSingle(row["Calories"]),
            Protein = Convert.ToSingle(row["Protein"]),
            Carbohydrates = Convert.ToSingle(row["Carbohydrates"]),
            Fats = Convert.ToSingle(row["Fats"])
        };

        return nutrition;
    }
}