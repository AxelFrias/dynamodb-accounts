namespace am.accounts.commons.Repository
{
    using am.accounts.commons.Models;

    public interface IAccountRepository
    {
        Task InsertAccountAsync(Account account);
        //Task InsertMetadataAsync(Metadata metadata);
        Task InsertSubaccountAsync(Subaccount subaccount);
    }
}
