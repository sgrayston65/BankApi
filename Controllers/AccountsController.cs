using BankApi.Applications.Dto;
using BankApi.Applications.Services;
using BankApi.Models;
using BankApi.Repositories;
using Google.Api;
using Microsoft.AspNetCore.Mvc;

namespace BankApi.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountRepository _accountRepo;
        private readonly ICustomerRepository _customerRepo;
        private readonly IAccountService _services;

        public AccountsController(IAccountRepository accountRepo, ICustomerRepository customerRepo, IAccountService services)
        {
            _accountRepo = accountRepo;
            _customerRepo = customerRepo;
            _services = services;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            //var account = await _accountRepo.GetAsync(id);
            var account = await _services.GetAsync(id);
            return account is null ? NotFound() : Ok(account);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Account account)
        {
            var customer = await _customerRepo.GetAsync(account.CustomerId);
            if (customer is null)
                return BadRequest("Invalid customer ID");

            await _accountRepo.CreateAsync(account);

            customer.AccountIds.Add(account.Id);

            return CreatedAtAction(nameof(Get), new { id = account.Id }, account);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Account account)
        {
            //account.Id = id;
            await _accountRepo.UpdateAsync(account);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _accountRepo.DeleteAsync(id);
            return NoContent();
        }
    }
}
