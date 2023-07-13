using MySql.Data.MySqlClient;
using NUnit.Framework;
using System.Data;
using WeightliftingTrackerGraphQLAPI.Data;

namespace WeightliftingTrackerGraphQLAPI.Tests
{
    [TestFixture]
    public class MySqlDataAccessTests
    {
        private const string ConnectionString = "SERVER=localhost;DATABASE=weightlifting_tracker;UID=root;PASSWORD=admin;";

        [Test]
        public void TestExecuteQuery()
        {
            string query = "SELECT * FROM Workout";
            MySqlParameter[] parameters = null;
            MySqlDataAccess dataAccess = new MySqlDataAccess(ConnectionString);

            try
            {
                DataTable result = dataAccess.ExecuteQuery(query, parameters);

                // Display the retrieved data
                Console.WriteLine("Workout Data:");
                foreach (DataRow row in result.Rows)
                {
                    Console.WriteLine($"Id: {row["Id"]}, ExerciseName: {row["ExerciseName"]}, Sets: {row["Sets"]}, Reps: {row["Reps"]}, Weight: {row["Weight"]}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error executing query: {ex.Message}");
            }
        }
    }
}