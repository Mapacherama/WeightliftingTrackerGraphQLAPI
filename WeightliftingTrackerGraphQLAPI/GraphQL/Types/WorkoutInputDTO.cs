namespace WeightliftingTrackerGraphQLAPI.GraphQL.Types
{
    public class WorkoutInputDTO
    {
        public string? ExerciseName { get; set; }

        public int Sets { get; set; }

        public int Reps { get; set; }

        public decimal Weight { get; set; }
    }
}
