using BankApi.Applications.Dto;
using BankApi.Models;
using BankApi.Repositories;
using System;
using System.Xml.Linq;

namespace BankApi.Applications.Services
{
    public class CustomerServiceCB : ICustomerService
    {
        private readonly ICustomerRepository _repository;
        private readonly IAccountService _accountService;

        public CustomerServiceCB(ICustomerRepository repository, IAccountService accountService)
        {
            _repository = repository;
            _accountService = accountService;
        }

        public async Task<Customer> CreateAsync(CustomerCreateDto customerDto)
        {
            var savedCustomer = new Customer(Guid.NewGuid().ToString(), customerDto.FirstName, customerDto.LastName);
            var newCustomer = await _repository.CreateAsync(savedCustomer);

            var newAccount = await _accountService.CreateAsync(new AccountCreateDto(newCustomer.Id, "Main Savings"));
            newCustomer.AccountIds.Add(newAccount.Id);
            await _repository.UpdateAsync(newCustomer);

            return newCustomer;
        }

        public async Task DeleteAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("Customer ID cannot be null or empty.", nameof(id));
            }
            
            var customer = await _repository.GetAsync(id);
            if (customer == null)
            {
                throw new KeyNotFoundException($"Customer with ID {id} not found.");
            }   

            foreach (var accountId in customer.AccountIds)
            {
                await _accountService.DeleteAsync(accountId);
            }

            await _repository.DeleteAsync(id);
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Customer> GetAsync(string id)
        {
            var customer = await _repository.GetAsync(id);
            if (customer == null)
            {
                throw new KeyNotFoundException($"Customer with ID {id} not found.");
            }
            return customer;
        }

        public async Task<Customer> UpdateAsync(Customer customer)
        {
            await _repository.UpdateAsync(customer);
            return customer;
        }
    }
}
