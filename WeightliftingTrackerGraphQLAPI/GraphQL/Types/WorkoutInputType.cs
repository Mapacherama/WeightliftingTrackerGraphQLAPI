namespace WeightliftingTrackerGraphQLAPI.GraphQL.Types
{
    public class WorkoutInputType
    {
        [GraphQLNonNullType]
        public string? ExerciseName { get; set; }

        [GraphQLNonNullType]
        public int Sets { get; set; }

        [GraphQLNonNullType]
        public int Reps { get; set; }

        [GraphQLNonNullType]
        public decimal Weight { get; set; }
    }

}
