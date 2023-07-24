using HotChocolate.Types;
using WeightliftingTrackerGraphQLAPI.Models;

namespace WeightliftingTrackerGraphQLAPI.GraphQL.Types
{
    public class UserInputType : InputObjectType<User>
    {
        protected override void Configure(IInputObjectTypeDescriptor<User> descriptor)
        {
            descriptor.Field(x => x.Username).Type<StringType>().Description("The username of the user.");
            descriptor.Field(x => x.Password).Type<StringType>().Description("The password the user uses to login.");
        }
    }
}
