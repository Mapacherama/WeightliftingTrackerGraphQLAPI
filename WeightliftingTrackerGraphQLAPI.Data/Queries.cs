namespace WeightliftingTrackerGraphQLAPI.Data
{
    public static class Queries
    {
        public const string QuerySelectAllWorkouts = "SELECT * FROM Workout";
        public const string QuerySelectWorkoutByDetails = "SELECT * FROM Workout WHERE ExerciseName = @ExerciseName AND Sets = @Sets AND Reps = @Reps AND Weight = @Weight;";
        public const string QuerySelectWorkoutById = "SELECT * FROM Workout WHERE Id = @Id;";
        public const string QuerySelectNutritionById = "SELECT * FROM Nutrition WHERE Id = @Id;";
        public const string MutationInsertNewWorkout = "INSERT INTO Workout (ExerciseName, Sets, Reps, Weight) VALUES (@ExerciseName, @Sets, @Reps, @Weight);";
        public const string MutationUpdateExistingWorkout = "UPDATE Workout SET ExerciseName = @ExerciseName, Sets = @Sets, Reps = @Reps, Weight = @Weight WHERE Id = @Id;";
        public const string MutationDeleteWorkout = "DELETE FROM Workout WHERE Id = @WorkoutId;";
        public const string MutationDeleteNutrition = "DELETE FROM Nutrition WHERE Id = @NutritionId;";
        public const string QuerySelectLastInsertedId = "SELECT LAST_INSERT_ID();";
        public const string QuerySelectUserByUsername = "SELECT * FROM User where Username = @Username";
        public const string MutationInsertNewUser = "INSERT INTO User(Username, Password) VALUES(@Username, @Password);";
    }
}
