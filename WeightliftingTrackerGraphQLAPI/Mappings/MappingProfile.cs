using AutoMapper;
using WeightliftingTrackerGraphQLAPI.Models;

namespace WeightliftingTrackerGraphQLAPI.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() { 
        
            CreateMap<Nutrition, NutritionDTO>();
            CreateMap<Hydration, HydrationDTO>();
            CreateMap<Workout, WorkoutDTO>();
            CreateMap<User, UserDTO>();
        }
    }
}
