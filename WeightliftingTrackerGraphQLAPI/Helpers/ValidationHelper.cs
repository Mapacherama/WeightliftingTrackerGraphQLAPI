namespace WeightliftingTrackerGraphQLAPI.Helpers
{
    public class ValidationHelper
    {
        public static void CheckIfNull<T>(T obj, string paramName) where T : class
        {
            if (obj == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }
    }
}
