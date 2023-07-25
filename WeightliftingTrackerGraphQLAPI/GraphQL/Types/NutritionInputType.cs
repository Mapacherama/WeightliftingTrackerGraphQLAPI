using WeightliftingTrackerGraphQLAPI.Models;

public class NutritionInputType : InputObjectType<Nutrition>
{
    protected override void Configure(IInputObjectTypeDescriptor<Nutrition> descriptor)
    {
        descriptor.Field(n => n.FoodName).Type<StringType>();
        descriptor.Field(n => n.Calories).Type<FloatType>();
        descriptor.Field(n => n.Protein).Type<FloatType>();
        descriptor.Field(n => n.Carbohydrates).Type<FloatType>();
        descriptor.Field(n => n.Fats).Type<FloatType>();
    }
}