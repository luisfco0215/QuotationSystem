using Microsoft.AspNetCore.Mvc;
using QuotationSystem.ContractRepository;
using QuotationSystem.Models;

namespace QuotationSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuoteController : ControllerBase
    {
        private readonly IQuoteRepository _quoteRepository;

        public QuoteController(IQuoteRepository repository)
        {
            _quoteRepository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var entities = await _quoteRepository.GetAllAsync();
            return Ok(entities);
        }

        [HttpPost]
        public async Task Add([FromBody] Quote request)
        {
            await _quoteRepository.AddAsync(request);
        }
    }
}
