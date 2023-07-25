using AutoMapper;
using WeightliftingTrackerGraphQLAPI.Models;

namespace WeightliftingTrackerGraphQLAPI.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() { 
        
            CreateMap<Nutrition, NutritionDTO>();
            CreateMap<Workout, WorkoutDTO>();
            CreateMap<User, UserDTO>();
        }
    }
}
