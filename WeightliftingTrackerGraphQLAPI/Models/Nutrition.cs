﻿namespace WeightliftingTrackerGraphQLAPI.Models
{
    public class Nutrition
    {
        public int Id { get; set; }
        public string? FoodName { get; set; }
        public float Calories { get; set; }
        public float Protein { get; set; }
        public float Carbohydrates { get; set; }
        public float Fats { get; set; }
    }
}
