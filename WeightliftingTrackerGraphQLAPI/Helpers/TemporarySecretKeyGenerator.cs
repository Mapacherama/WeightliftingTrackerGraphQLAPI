using System.Security.Cryptography;

namespace WeightliftingTrackerGraphQLAPI.Helpers
{
    public static class TemporarySecretKeyGenerator
    {
        public static string GenerateTemporarySecretKey(int length = 32)
        {
            var bytes = new byte[length];
            using var randomGenerator = RandomNumberGenerator.Create();
            randomGenerator.GetBytes(bytes);
            return Convert.ToBase64String(bytes);
        }
    }
}
