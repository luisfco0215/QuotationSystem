using QuotationSystem.Models;
using System.Data.SqlClient;

namespace QuotationSystem.DbConnections
{
    public class DbConnection(string connectionString)
    {

        public SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
