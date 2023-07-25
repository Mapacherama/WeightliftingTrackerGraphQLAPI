namespace WeightliftingTrackerGraphQLAPI.GraphQL.Types
{
    public class HydrationInputDTO
    {
        public int UserId { get; set; }
        public float waterIntake { get; set; }
        public float HydrationGoal { get; set; }
    }
}
