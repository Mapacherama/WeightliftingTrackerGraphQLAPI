using HotChocolate.Types;
using WeightliftingTrackerGraphQLAPI.Models;

namespace WeightliftingTrackerGraphQLAPI.GraphQL.Types
{
    public class NutritionInputType : InputObjectType<Nutrition>
    {
        protected override void Configure(IInputObjectTypeDescriptor<Nutrition> descriptor)
        {
            descriptor.Field(n => n.Calories).Type<NonNullType<FloatType>>();
            descriptor.Field(n => n.Protein).Type<NonNullType<FloatType>>();
            descriptor.Field(n => n.Carbohydrates).Type<NonNullType<FloatType>>();
            descriptor.Field(n => n.Fats).Type<NonNullType<FloatType>>();
        }
    }
}