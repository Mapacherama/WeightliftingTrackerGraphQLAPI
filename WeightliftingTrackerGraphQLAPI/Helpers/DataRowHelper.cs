using System.Data;

namespace WeightliftingTrackerGraphQLAPI.Helpers
{
    public class DataRowHelper
    {
        public static void CheckDataRow(DataRow row, params string[] columnNames)
        {
            foreach (var columnName in columnNames)
            {
                if (row.IsNull(columnName))
                {
                    throw new NullReferenceException($"The value for {columnName} is null.");
                }
            }
        }
    }
}
