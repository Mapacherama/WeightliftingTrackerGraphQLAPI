using Helpers;

namespace SecretKeyGenerator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string secretKey = TemporarySecretKeyGenerator.GenerateTemporarySecretKey();
            Console.WriteLine(secretKey);
        }
    }
}
