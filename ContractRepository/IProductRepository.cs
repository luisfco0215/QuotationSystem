using QuotationSystem.Models;

namespace QuotationSystem.ContractRepository
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task AddAsync(Product request);
    }
}
