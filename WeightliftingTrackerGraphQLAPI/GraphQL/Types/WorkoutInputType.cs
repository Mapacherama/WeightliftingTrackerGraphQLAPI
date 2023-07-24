using HotChocolate.Types;
using WeightliftingTrackerGraphQLAPI.Models;

namespace WeightliftingTrackerGraphQLAPI.GraphQL.Types
{
    public class WorkoutInputType : InputObjectType<Nutrition>
    {
        protected override void Configure(IInputObjectTypeDescriptor<Nutrition> descriptor)
        {
            descriptor.Field(w => w.ExerciseName).Type<NonNullType<StringType>>();
            descriptor.Field(w => w.Sets).Type<NonNullType<IntType>>();
            descriptor.Field(w => w.Reps).Type<NonNullType<IntType>>();
            descriptor.Field(w => w.Weight).Type<NonNullType<FloatType>>();
        }
    }
}
