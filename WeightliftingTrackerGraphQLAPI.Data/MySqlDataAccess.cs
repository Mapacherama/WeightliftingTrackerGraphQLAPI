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

        public async Task<DataTable> ExecuteQueryAsync(string query, MySqlParameter[] parameters )
        {
            DataTable resultTable = new DataTable();

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        resultTable.Load(reader);
                    }
                }
            }

            return resultTable;
        }

        public async Task<object> ExecuteScalarAsync(string query, MySqlParameter[] parameters)
        {
            object result = null;

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    result = await command.ExecuteScalarAsync();
                }
            }

            return result;
        }
    }
}
