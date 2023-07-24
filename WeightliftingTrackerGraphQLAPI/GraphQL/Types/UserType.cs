using HotChocolate.Types;
using WeightliftingTrackerGraphQLAPI.Models;

namespace WeightliftingTrackerGraphQLAPI.GraphQL.Types
{
    public class UserType : ObjectType<User>
    {        
        protected override void Configure(IObjectTypeDescriptor<User>descriptor)
        {
            descriptor.Field(x => x.Id).Type<IdType>().Description("The unique identifier of the user.");
            descriptor.Field(x => x.Username).Type<StringType>().Description("The username of the user.");
            descriptor.Field(x => x.Password).Type<StringType>().Description("The password the user uses to login.");
        }
    }
}
