using Microsoft.AspNetCore.Mvc;
using QuotationSystem.ContractRepository;
using QuotationSystem.Models;

namespace QuotationSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository repository)
        {
            _productRepository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var entities = await _productRepository.GetAllAsync();
            return Ok(entities);
        }

        [HttpPost]
        public async Task Add([FromBody] Product request)
        {
            await _productRepository.AddAsync(request);
        }
    }
}
