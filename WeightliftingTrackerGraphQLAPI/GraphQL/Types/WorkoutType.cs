using HotChocolate.Types;
using WeightliftingTrackerGraphQLAPI.Models;

public class WorkoutType : ObjectType<Nutrition>
{
    protected override void Configure(IObjectTypeDescriptor<Nutrition> descriptor)
    {
        descriptor.Field(x => x.Id).Type<IdType>().Description("The unique identifier of the workout.");
        descriptor.Field(x => x.ExerciseName).Type<StringType>().Description("The name of the exercise.");
        descriptor.Field(x => x.Sets).Description("The number of sets performed in the workout.");
        descriptor.Field(x => x.Reps).Description("The number of reps performed in each set.");
        descriptor.Field(x => x.Weight).Description("The weight used in each set.");
    }
}
