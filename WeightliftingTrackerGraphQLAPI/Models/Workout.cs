namespace WeightliftingTrackerGraphQLAPI.Models
{
    public class Workout
    {
        public int Id { get; set; }
        public string? ExerciseName { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public decimal Weight { get; set; }
    }
}
