using MySql.Data.MySqlClient;
using System.Data;

namespace WeightliftingTrackerGraphQLAPI.Data
{
    public interface IMySqlDataAccess
    {
        DataTable ExecuteQuery(string query, MySqlParameter[] parameters);
        object ExecuteScalar(string query, MySqlParameter[] parameters);
    }
}
