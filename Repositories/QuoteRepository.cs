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
            var connection = new SqlConnection(_connectionString);

            await connection.OpenAsync();

            string query = @"INSERT INTO Quote (Client, PhoneNumber, TotalAmount, [Date]) 
                             VALUES (@Client, @PhoneNumber, @TotalAmount, @Date);
                             SELECT SCOPE_IDENTITY();";

            var command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Client", quote.Client);
            command.Parameters.AddWithValue("@PhoneNumber", quote.PhoneNumber);
            command.Parameters.AddWithValue("@TotalAmount", quote.TotalAmount);
            command.Parameters.AddWithValue("@Date", quote.Date);

            await command.ExecuteScalarAsync();
        }
    }
}
