using Grpc.Net.Client.Balancer;

namespace BankApi.Models
{
    public class SavingsAccount : SubAccount
    {
        public SavingsAccount()
        {
            Type = "Savings";
            Nickname = string.Empty; 
        }

        public SavingsAccount(string nickname, decimal balance = 0)
        {
            Nickname = nickname;
            Type = "Savings";
            Balance = balance;
        }

        public string Nickname { get; set; }
    }
}