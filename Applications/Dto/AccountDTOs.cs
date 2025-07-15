namespace BankApi.Applications.Dto
{
    public record AccountCreateDto(string CustomerId, string Nickname, decimal Balance = 0);
}
