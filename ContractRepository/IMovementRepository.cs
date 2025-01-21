using QuotationSystem.Models;

namespace QuotationSystem.ContractRepository
{
    public interface IMovementRepository
    {
        Task<IEnumerable<Movement>> GetAllAsync();
        Task AddAsync(Movement request);
    }
}
