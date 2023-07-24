using WeightliftingTrackerGraphQLAPI.Models;

namespace WeightliftingTrackerGraphQLAPI.Repositories
{
    public interface INutritionRepository
    {
        Task<IEnumerable<NutritionDTO>> GetWorkouts();
        Task<Nutrition> CreateNutrition(Nutrition newNutrition);
        Task<Nutrition> UpdateNutrition(Nutrition updatedNutrition);
        Task<Nutrition> DeleteNutrition(int nutritionId);
    }
}
