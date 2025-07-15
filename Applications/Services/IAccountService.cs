using BankApi.Applications.Dto;
using BankApi.Models;

namespace BankApi.Applications.Services
{
    public interface IAccountService
    {
        Task<Account> CreateAsync(AccountCreateDto creatDto);
        //Task<List<Account>> GetAllAsync();
        Task<Account?> GetAsync(string id);
        //Task UpdateAsync(AccountUpdateDto accountDto);
        Task DeleteAsync(string id);
    }
}