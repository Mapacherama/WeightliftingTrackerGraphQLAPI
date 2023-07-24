using WeightliftingTrackerGraphQLAPI.Models;

namespace WeightliftingTrackerGraphQLAPI.Repositories
{
    public class NutritionRepository : INutritionRepository
    {
        public Task<Nutrition> CreateNutrition(Nutrition newNutrition)
        {
            throw new NotImplementedException();
        }

        public Task<Nutrition> DeleteNutrition(int nutritionId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<NutritionDTO>> GetWorkouts()
        {
            throw new NotImplementedException();
        }

        public Task<Nutrition> UpdateNutrition(Nutrition updatedNutrition)
        {
            throw new NotImplementedException();
        }
    }
}
