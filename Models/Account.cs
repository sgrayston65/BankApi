namespace BankApi.Models
{
    public class Account
    {
        public Account()
        {
            SubAccounts = new List<SubAccount>();
        }

        public Account(string id, string customerId, SubAccount subAccount)
        {
            Id = id;
            CustomerId = customerId;
            SubAccounts.Add(subAccount);
        }

        public string Id { get; set; }
        public string CustomerId { get; set; }
        public List<SubAccount> SubAccounts { get; set; } = new();
        public string Type => "account";
    }
}
