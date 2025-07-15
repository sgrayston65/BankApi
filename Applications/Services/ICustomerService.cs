using BankApi.Applications.Dto;
using BankApi.Models;

namespace BankApi.Applications.Services
{
    public interface ICustomerService
    {
        Task<Customer> CreateAsync(CustomerCreateDto customerDto);
        Task<Customer> UpdateAsync(Customer customer);
        Task<List<Customer>> GetAllAsync();
        Task<Customer> GetAsync(string id);
        Task DeleteAsync(string id);
    }
}