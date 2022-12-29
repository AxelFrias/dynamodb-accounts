// See https://aka.ms/new-console-template for more information
// See https://aka.ms/new-console-template for more information
using am.accounts.commons.Models;
using am.accounts.commons.Repository;

Console.WriteLine("TESTING!");
var accountRepository = new AccountRepository();

Random rnd = new();
for (int i = 0; i < 100; i++)
{
    Console.WriteLine($"Account {i + 1}!");

    var account = new Account
    {
        Id = Guid.NewGuid(),
        OrgId = Guid.NewGuid(),
        CorporationId = Guid.NewGuid(),
        CurrencyCode = "MXN",
        EndingIn = rnd.Next(80000, 83000).ToString(),
        Type = rnd.Next(0, 2) == 0 ? "CLABE" : "CREDIT",
        Status = rnd.Next(0, 2) == 0 ? "CREATED" : "DELETED",
        OwnershipType = rnd.Next(0, 2) == 0 ? "OWNER" : "THIRD_PARTY"
    };
    Console.WriteLine("Account:");
    Console.WriteLine(account);

    await accountRepository.InsertAccountAsync(account);
}

Console.WriteLine("Check results!");