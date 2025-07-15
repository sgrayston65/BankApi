using BankApi.Applications.Dto;
using BankApi.Models;
using BankApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using BankApi.Applications.Services;

namespace BankApi.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _repo;
        private readonly ICustomerService _services;
        private readonly ILogger<CustomersController> _logger;

        public CustomersController(ICustomerRepository repo, ICustomerService services, ILogger<CustomersController> logger)
        {
            _repo = repo;
            _services = services;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _services.GetAllAsync();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var customer = await _services.GetAsync(id);
            return customer is null ? NotFound() : Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomerCreateDto customerCreateDto)
        {
            try
            {
                _logger.LogInformation("Creating customer: {@CustomerCreateDto}", customerCreateDto);
                var newCustomer = await _services.CreateAsync(customerCreateDto);
                _logger.LogInformation("Customer created: {@Customer}", newCustomer);
                return CreatedAtAction(nameof(Get), new { id = newCustomer.Id }, newCustomer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating customer");
                return StatusCode(500, "An internal server error occurred.");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(Customer customer)
        {
            await _repo.UpdateAsync(customer);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _services.DeleteAsync(id);
            return NoContent();
        }

    }
}
