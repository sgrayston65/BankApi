namespace BankApi.Models
{
    abstract public class SubAccount
    {
        //public SubAccount(string type, decimal balance = 0)
        //{
        //    Type = type;
        //    Balance = balance;
        //}
        public string Type { get; set; } = string.Empty; // e.g., "Checking", "Savings"  
        public decimal Balance { get; set; }
    }
}
