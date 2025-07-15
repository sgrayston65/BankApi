using System.ComponentModel.DataAnnotations;

namespace BankApi.Models
{
    public class Customer
    {
        [Required]
        public string Id { get; set; }
        
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public List<string> AccountIds { get; set; } = new();

        [Required]
        public string Type => "customer";

        public Customer(string id, string firstName, string lastName)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
