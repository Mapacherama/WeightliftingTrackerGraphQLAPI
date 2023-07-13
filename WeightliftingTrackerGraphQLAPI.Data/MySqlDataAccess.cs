using MySql.Data.MySqlClient;
using System.Data;

namespace WeightliftingTrackerGraphQLAPI.Data
{
    public class MySqlDataAccess : IMySqlDataAccess
    {
        private readonly string _connectionString;

        public MySqlDataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DataTable ExecuteQuery(string query, MySqlParameter[] parameters )
        {
            DataTable resultTable = new DataTable();

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        resultTable.Load(reader);
                    }
                }
            }

            return resultTable;
        }

        public object ExecuteScalar(string query, MySqlParameter[] parameters)
        {
            object result = null;

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    result = command.ExecuteScalar();
                }
            }

            return result;
        }
    }
}
