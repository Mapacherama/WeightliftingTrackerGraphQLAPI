namespace WeightliftingTrackerGraphQLAPI.GraphQL.Types
{
    public class NutritionInputDTO
    {
        public string? FoodName { get; set; }
        public float Calories { get; set; }
        public float Protein { get; set; }
        public float Carbohydrates { get; set; }
        public float Fats { get; set; }
    }
}
