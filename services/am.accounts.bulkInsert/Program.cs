// See https://aka.ms/new-console-template for more information
using am.accounts.commons.Models;
using am.accounts.commons.Repository;
using am_accounts_BulkInsert.Models;

Console.WriteLine("TESTING!");
var accountRepository = new AccountRepository();

Random rnd = new();
for (int i = 0; i < 2; i++)
{
    Console.WriteLine($"Account {i + 1}!");

    var account = new Account
    {
        Id = Guid.NewGuid(),
        OrgId = rnd.Next(0, 2) == 0 ? Constants.OrgId1 : Constants.OrgId2,
        CorporationId = rnd.Next(0, 2) == 0 ? Constants.Corp1 : Constants.Corp2,
        CurrencyCode = "MXN",
        EndingIn = rnd.Next(80000, 83000).ToString(),
        Type = rnd.Next(0, 2) == 0 ? "CLABE" : "CREDIT",
        Status = rnd.Next(0, 2) == 0 ? "CREATED" : "DELETED",
        OwnershipType = rnd.Next(0, 2) == 0 ? "OWNER" : "THIRD_PARTY",
        CreatedAt = CreateRandomDate()
    };


    await accountRepository.InsertAccountAsync(account);
}

DateTime CreateRandomDate()
{
    var randomYear = rnd.Next(2019, 2022);
    var randomMonth = rnd.Next(1, 12);
    var randomDay = rnd.Next(1, 30);
    return new DateTime(randomYear, randomMonth, randomDay);
};

Console.WriteLine("Check results!");
