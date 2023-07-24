using AutoMapper;
using WeightliftingTrackerGraphQLAPI.Repositories;

namespace WeightliftingTrackerGraphQLAPI.Resolvers
{
    public class NutritionResolvers
    {
        private readonly INutritionRepository _nutritionRepository;
        private readonly IMapper _mapper;

        public NutritionResolvers(INutritionRepository nutritionRepository, IMapper mapper)
        {
            _nutritionRepository = nutritionRepository;
            _mapper = mapper;
        }

        // TODO CRUD Functionality.
    }
}
