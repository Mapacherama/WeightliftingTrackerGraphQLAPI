using HotChocolate.Types;
using WeightliftingTrackerGraphQLAPI.Models;

namespace WeightliftingTrackerGraphQLAPI.GraphQL.Types
{
    public class HydrationType : ObjectType<Hydration>
    {
        protected override void Configure(IObjectTypeDescriptor<Hydration> descriptor)
        {
            descriptor.Description("Represents the hydration data.");

            descriptor.Field(h => h.Id)
                .Description("The unique identifier for the hydration record.");

            descriptor.Field(h => h.UserId)
                .Description("The user's ID associated with the hydration record.");

            descriptor.Field(h => h.waterIntake)
                .Description("The amount of water intake for the hydration record.");

            descriptor.Field(h => h.HydrationGoal)
                .Description("The hydration goal for the user.");
        }
    }
}
