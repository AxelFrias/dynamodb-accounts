// See https://aka.ms/new-console-template for more information
using am.accounts.commons.Models;
using am.accounts.commons.Repository;

Console.WriteLine("TESTING!");
var accountRepository = new AccountRepository();

Random rnd = new();
var account = new Account
{
    Id = Guid.Parse("4c0e400e-9575-489f-82b9-e503aa63f362"),
    OrgId = Guid.Parse("e4c82a64-9bac-45a6-94ab-0af6352f01cb"),
    CorporationId = Guid.Parse("13afc650-1d5c-4f71-a164-55209b11d299"),
    CurrencyCode = "MXN",
    EndingIn = rnd.Next(80000, 83000).ToString(),
    Type = "CLABE",
    Status = "CREATED",
    OwnershipType = "THIRD_PARTY",
    CreatedAt = CreateRandomDate(),
    LoremContractId = Guid.NewGuid(),
    ExternalId = CreateExternalId(),
    Number = Guid.NewGuid(),
    PartitionKey = "e4c82a64-9bac-45a6-94ab-0af6352f01cb#13afc650-1d5c-4f71-a164-55209b11d299#4c0e400e-9575-489f-82b9-e503aa63f362",
    OrgCorp = "e4c82a64-9bac-45a6-94ab-0af6352f01cb#13afc650-1d5c-4f71-a164-55209b11d299"
};

await accountRepository.UpdateAccountAsync(account);


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
