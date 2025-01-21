using QuotationSystem.ContractRepository;
using QuotationSystem.DbConnections;
using QuotationSystem.Models;
using System.Data.SqlClient;

namespace QuotationSystem.Repositories
{
    public class MovementRepository : IMovementRepository
    {
        private readonly DbConnection _dbConnection;

        public MovementRepository(string connecitonString)
        {
            _dbConnection = new DbConnection(connecitonString);
        }

        public async Task<IEnumerable<Movement>> GetAllAsync()
        {
            var movements = new List<Movement>();

            var connection = _dbConnection.GetConnection();

            connection.Open();

            string query = "SELECT MovementID, ProductID, QuoteID, Quantity, TotalAmount FROM Movement";

            var command = new SqlCommand(query, connection);

            var reader = command.ExecuteReader();


            while (reader.Read())
            {
                movements.Add(new Movement
                {
                    MovementID = reader.GetInt32(0),
                    ProductID = reader.GetInt32(1),
                    QuoteID = reader.GetInt32(2),
                    Quantity = reader.GetDecimal(3),
                    TotalAmount = reader.GetDecimal(4)
                });
            }

            connection.Close();

            return movements;
        }

        public async Task AddAsync(Movement request)
        {
            var connection = _dbConnection.GetConnection();

            connection.Open();

            string query = "INSERT INTO Movement (ProductID, QuoteID, Quantity, TotalAmount) " +
                           "VALUES (@ProductID, @QuoteID, @Quantity, @TotalAmount)";

            var command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ProductID", request.ProductID);
            command.Parameters.AddWithValue("@QuoteID", request.QuoteID);
            command.Parameters.AddWithValue("@Quantity", request.Quantity);
            command.Parameters.AddWithValue("@TotalAmount", request.TotalAmount);

            command.ExecuteNonQuery();

            connection.Close();
        }
    }

}
