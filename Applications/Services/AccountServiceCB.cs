using BankApi.Applications.Dto;
using BankApi.Models;
using BankApi.Repositories;
using System.Security.Principal;

namespace BankApi.Applications.Services
{
    public class AccountServiceCB : IAccountService
    {
        private readonly IAccountRepository _repository;

        public AccountServiceCB(IAccountRepository repository)
        {
            _repository = repository;
        }

        public async Task<Account> CreateAsync(AccountCreateDto creatDto)
        {
            SubAccount savingsAccount = new SavingsAccount(creatDto.Nickname);
            var accountNumber = await GenerateUnique8DigitNumber();
            var newAccount = new Account(accountNumber, creatDto.CustomerId, savingsAccount);

            await _repository.CreateAsync(newAccount);

            return newAccount;
        }

        public async Task DeleteAsync(string id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<Account?> GetAsync(string id)
        {
            return await _repository.GetAsync(id);
        }

        public async Task<string> GenerateUnique8DigitNumber()
        {
            var random = new Random();
            string number;
            do
            {
                number = random.Next(10000000, 100000000).ToString(); // Generates a number between 10000000 and 99999999
            } while (await IsIdUsed(number)); 

            return number;
        }

        private async Task<bool> IsIdUsed(string number)
        {

            var acc = await _repository.GetAsync(number);
            return (acc != null);
        }
    }

}
