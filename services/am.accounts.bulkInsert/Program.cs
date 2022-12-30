// See https://aka.ms/new-console-template for more information
using am.accounts.commons.Models;
using am.accounts.commons.Repository;
using am_accounts_BulkInsert.Models;

Console.WriteLine("TESTING!");
var accountRepository = new AccountRepository();

Random rnd = new();
for (int i = 0; i < 1; i++)
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
        CreatedAt = CreateRandomDate(),
        LoremContractId = Guid.NewGuid(),
        ExternalId = CreateExternalId(),
        Number = Guid.NewGuid(),
    };

    account.PartitionKey = $"{account.OrgId}#{account.CorporationId}#{account.Id}";
    account.OrgCorp = $"{account.OrgId}#{account.CorporationId}";

    await accountRepository.InsertAccountAsync(account);
}

string CreateExternalId()
{
    string firstCharacters = string.Empty;
    string lastCharacters = rnd.Next(100, 999).ToString();
    switch (rnd.Next(1, 3))
    {
        case 1:
            firstCharacters = "AAA";
            break;
        case 2:
            firstCharacters = "ABA";
            break;
        case 3:
            firstCharacters = "ABC";
            break;
        default:
            firstCharacters = "DBC";
            break;
    }
    return $"{firstCharacters}{lastCharacters}";
}

DateTime CreateRandomDate()
{
    var randomYear = rnd.Next(2019, 2022);
    var randomMonth = rnd.Next(1, 12);
    var randomDay = rnd.Next(1, 30);
    return new DateTime(randomYear, randomMonth, randomDay);
};

Console.WriteLine("Check results!");
