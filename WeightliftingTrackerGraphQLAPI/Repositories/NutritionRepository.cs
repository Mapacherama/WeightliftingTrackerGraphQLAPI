using AutoMapper;
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

            // Implement your logic here

            return newNutrition;
        }

        public Task<Nutrition> DeleteNutrition(int nutritionId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<NutritionDTO>> GetNutritions()
        {
            throw new NotImplementedException();
        }

        public Task<Nutrition> UpdateNutrition(Nutrition updatedNutrition)
        {
            throw new NotImplementedException();
        }
    }
}
