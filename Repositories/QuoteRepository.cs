using QuotationSystem.Models;
using System.Data.SqlClient;

namespace QuotationSystem.Repositories
{
    public class QuoteRepository
    {
        private readonly string _connectionString;

        public QuoteRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<int> AddQuoteAsync(Quote quote)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = @"INSERT INTO Quote (Client, PhoneNumber, TotalAmount, [Date]) 
                             VALUES (@Client, @PhoneNumber, @TotalAmount, @Date);
                             SELECT SCOPE_IDENTITY();";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Client", quote.Client);
                    command.Parameters.AddWithValue("@PhoneNumber", quote.PhoneNumber);
                    command.Parameters.AddWithValue("@TotalAmount", quote.TotalAmount);
                    command.Parameters.AddWithValue("@Date", quote.Date);

                    // Ejecutar la consulta y obtener el ID generado
                    object result = await command.ExecuteScalarAsync();

                    return Convert.ToInt32(result); // Retorna el ID del registro insertado
                }
            }
        }
    }
}
