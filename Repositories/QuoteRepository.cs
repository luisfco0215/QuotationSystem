using QuotationSystem.ContractRepository;
using QuotationSystem.Models;
using System.Data.SqlClient;

namespace QuotationSystem.Repositories
{
    public class QuoteRepository : IQuoteRepository
    {
        private readonly string _connectionString;

        public QuoteRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Quote>> GetAllAsync()
        {
            var quotes = new List<Quote>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT QuoteID, Client, PhoneNumber, TotalAmount, [Date] FROM Quote";

                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            quotes.Add(new Quote
                            {
                                QuoteID = reader.GetInt32(0),
                                Client = reader.GetString(1),
                                PhoneNumber = reader.GetString(2),
                                TotalAmount = reader.IsDBNull(3) ? 0 : reader.GetDecimal(3),
                                Date = reader.IsDBNull(4) ? DateTime.MinValue : reader.GetDateTime(4)
                            });
                        }
                    }
                }
            }

            return quotes;
        }

        public async Task AddAsync(Quote quote)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = @"INSERT INTO Quote (Client, PhoneNumber, TotalAmount, [Date]) 
                         VALUES (@Client, @PhoneNumber, @TotalAmount, @Date);
                         SELECT CAST(SCOPE_IDENTITY() AS INT);";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Client", quote.Client);
                    command.Parameters.AddWithValue("@PhoneNumber", quote.PhoneNumber);
                    command.Parameters.AddWithValue("@TotalAmount", quote.TotalAmount);
                    command.Parameters.AddWithValue("@Date", quote.Date);

                    quote.QuoteID = (int)await command.ExecuteScalarAsync();
                }

                foreach (var item in quote.Movements)
                {
                    item.QuoteID = quote.QuoteID;
                    await AddMovementAsync(item, connection).ConfigureAwait(false);
                }
            }
        }

        public async Task AddMovementAsync(Movement movement, SqlConnection connection)
        {
            string query = @"INSERT INTO Movement (ProductID, QuoteID, Quantity, TotalAmount) 
                     VALUES (@ProductID, @QuoteID, @Quantity, @TotalAmount)";

            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ProductID", movement.ProductID);
                command.Parameters.AddWithValue("@QuoteID", movement.QuoteID);
                command.Parameters.AddWithValue("@Quantity", movement.Quantity);
                command.Parameters.AddWithValue("@TotalAmount", movement.TotalAmount);

                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
