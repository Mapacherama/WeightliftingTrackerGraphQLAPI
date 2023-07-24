using HotChocolate.Types;
using WeightliftingTrackerGraphQLAPI.Models;

public class NutritionType : ObjectType<Nutrition>
{
    protected override void Configure(IObjectTypeDescriptor<Nutrition> descriptor)
    {
        descriptor.Field(x => x.Id).Type<IdType>().Description("The unique identifier of the nutrition.");
        descriptor.Field(x => x.FoodName).Type<StringType>().Description("The name of the food.");
        descriptor.Field(x => x.Calories).Description("The number of calories in the food.");
        descriptor.Field(x => x.Protein).Description("The amount of protein in the food.");
        descriptor.Field(x => x.Carbohydrates).Description("The amount of carbohydrates in the food.");
        descriptor.Field(x => x.Fats).Description("The amount of fats in the food.");
    }
}
