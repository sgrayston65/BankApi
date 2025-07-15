using BankApi.Models;

namespace BankApi.Repositories
{
    public interface IAccountRepository
    {
        Task<Account?> GetAsync(string id);
        Task CreateAsync(Account account);
        Task UpdateAsync(Account account);
        Task DeleteAsync(string id);
    }
}
