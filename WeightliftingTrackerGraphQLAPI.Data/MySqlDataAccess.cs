using MySql.Data.MySqlClient;
using System.Data;

namespace WeightliftingTrackerGraphQLAPI.Data
{
    public class MySqlDataAccess
    {
        private readonly string _connectionString;

        public MySqlDataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DataTable ExecuteQuery(string query)
        {
            DataTable resultTable = new DataTable();

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        resultTable.Load(reader);
                    }
                }
            }

            return resultTable;
        }
    }
}
