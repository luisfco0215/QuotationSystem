using QuotationSystem.Models;

namespace QuotationSystem.ContractRepository
{
    public interface IQuoteRepository
    {
        Task<List<Quote>> GetAllAsync();
        Task AddAsync(Quote request);
    }
}
