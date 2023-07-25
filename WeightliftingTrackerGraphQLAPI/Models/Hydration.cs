namespace WeightliftingTrackerGraphQLAPI.Models
{
    public class Hydration
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public float waterIntake { get; set; }
        public float HydrationGoal { get; set; }
    }
}
