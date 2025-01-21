using QuotationSystem.Models;

namespace QuotationSystem.ContractRepository
{
    public interface IQuoteRepository
    {
        Task<IEnumerable<Quote>> GetAllAsync();
        Task AddAsync(Quote request);
    }
}
