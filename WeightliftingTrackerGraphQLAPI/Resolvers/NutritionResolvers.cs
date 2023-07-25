using WeightliftingTrackerGraphQLAPI.Models;
using WeightliftingTrackerGraphQLAPI.Repositories;

public class NutritionResolvers
{
    private readonly INutritionRepository _nutritionRepository;

    public NutritionResolvers(INutritionRepository nutritionRepository)
    {
        _nutritionRepository = nutritionRepository;
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

    public async Task<Nutrition> getNutritionById(int id)
    {
        return await _nutritionRepository.GetNutritionById(id);
    }
}