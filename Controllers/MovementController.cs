using Microsoft.AspNetCore.Mvc;
using QuotationSystem.ContractRepository;
using QuotationSystem.Models;
using System.Security.Principal;

namespace QuotationSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovementController : ControllerBase
    {
        private readonly IMovementRepository _movementRepository;

        public MovementController(IMovementRepository repository)
        {
            _movementRepository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var entities = await _movementRepository.GetAllAsync();
            return Ok(entities);
        }

        [HttpPost]
        public async Task Add([FromBody] Movement request)
        {
            await _movementRepository.AddAsync(request);
        }
    }
}
