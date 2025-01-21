using QuotationSystem.ContractRepository;
using QuotationSystem.DbConnections;
using QuotationSystem.Models;
using System.Data.SqlClient;

namespace QuotationSystem.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DbConnection _dbConnection;
        public ProductRepository(string connecitonString)
        {
            _dbConnection = new DbConnection(connecitonString);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var products = new List<Product>();

            var connection = _dbConnection.GetConnection();

            connection.Open();

            var command = new SqlCommand("SELECT * FROM Product", connection);

            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                products.Add(new Product
                {
                    ProductID = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Description = reader.IsDBNull(2) ? null : reader.GetString(2),
                    Code = reader.GetString(3),
                    Cost = reader.GetDecimal(4),
                    Price = reader.IsDBNull(5) ? null : reader.GetDecimal(5),
                    Quantity = reader.IsDBNull(6) ? null : reader.GetInt32(6),
                });
            }

            connection.Close();

            return products;
        }

        public async Task AddAsync(Product product)
        {
            var connection = _dbConnection.GetConnection();

            connection.Open();

            var command = new SqlCommand("INSERT INTO Product (Name, Description, Code, Cost, Price, Quantity) " +
                $"VALUES (@Name, @Description, @Code, @Cost, @Price, @Quantity)", connection);

            command.Parameters.AddWithValue("@Name", product.Name);
            command.Parameters.AddWithValue("@Description", product.Description ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@Code", product.Code);
            command.Parameters.AddWithValue("@Cost", product.Cost);
            command.Parameters.AddWithValue("@Price", product.Price ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@Quantity", product.Quantity ?? (object)DBNull.Value);

            command.ExecuteNonQuery();

            connection.Close();
        }
    }

}