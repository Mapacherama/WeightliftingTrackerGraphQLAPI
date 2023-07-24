﻿using WeightliftingTrackerGraphQLAPI.Models;

namespace WeightliftingTrackerGraphQLAPI.GraphQL.Types
{
    public class NutritionType : ObjectType<Nutrition>
    {
        protected override void Configure(IObjectTypeDescriptor<Nutrition> descriptor)
        {
            descriptor.Field(n => n.FoodName).Type<NonNullType<StringType>>();
            descriptor.Field(n => n.Calories).Type<NonNullType<FloatType>>();
            descriptor.Field(n => n.Protein).Type<NonNullType<FloatType>>();
            descriptor.Field(n => n.Carbohydrates).Type<NonNullType<FloatType>>();
            descriptor.Field(n => n.Fats).Type<NonNullType<FloatType>>();
        }
    }
}
