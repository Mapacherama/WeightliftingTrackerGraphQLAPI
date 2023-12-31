﻿namespace WeightliftingTrackerGraphQLAPI.Helpers
{
    public class ErrorMessages
    {
        public const string DataAccessIsNull = "_dataAccess is null";
        public const string DataTableIsNull = "Data table is null";
        public const string RowValueIsNull = "One of the row values is null";
        public const string WorkoutDetailsExists = "A workout with the same details already exists.";
        public const string NutritionDetailsExists = "A nutrition with the same details already exists.";
        public const string UsernameExists = "A user with the same details already exists.";
    }
}
