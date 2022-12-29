using am.accounts.commons.Models;

namespace am.accounts.commons.Repository
{
    using Amazon.DynamoDBv2;
    using Amazon.DynamoDBv2.DataModel;
    using Amazon.DynamoDBv2.Model;

    public class AccountRepository : IAccountRepository
    {
        public async Task InsertAccountAsync(Account account)
        {
            var client = new AmazonDynamoDBClient();

            await BatchWriteAccountsAsync(client, account);
        }

        private static async Task BatchWriteAccountsAsync(AmazonDynamoDBClient client, Account account)
        {
            var context = new DynamoDBContext(client);
            var tableName = "am-accounts-test";

            var request = new BatchWriteItemRequest
            {
                RequestItems = new Dictionary<string, List<WriteRequest>>
                {
                  {
                    tableName, CreateAccountBatchInsert(account)
                  } ,
                },
                ReturnItemCollectionMetrics = ReturnItemCollectionMetrics.SIZE,
            };
            var response = await client.BatchWriteItemAsync(request);
        }

        private static List<WriteRequest> CreateAccountBatchInsert(Account account)
        {

            var request = new List<WriteRequest>();

            var filters = CreateFilters(account);

            foreach (var filter in filters)
            {
                account.PartitionKey = AccountKey.CreateForIndexId(account.OrgId, account.CorporationId, account.Id, filter).ToString();
                request.Add(new WriteRequest
                {
                    PutRequest = new PutRequest
                    {
                        Item = AccountToDictionary(account, filter)
                    },
                });
            }
            
            return request;
        }

        private static Dictionary<string, AttributeValue> AccountToDictionary(Account account, string filter)
        {
            return new Dictionary<string, AttributeValue>
            {
                { "partition_key", new AttributeValue { S = account.PartitionKey } },
                { "resource", new AttributeValue { S = string.Concat("ACCOUNT", filter) }},
                { "corporation_id", new AttributeValue { S = account.CorporationId.ToString() }},
                { "created_at", new AttributeValue { S = account.CreatedAt.ToString() }},
                { "currency_code", new AttributeValue { S = account.CurrencyCode }},
                { "ending_in", new AttributeValue { S = account.EndingIn }},
                { "id", new AttributeValue { S = account.Id.ToString() }},
                { "org_id", new AttributeValue { S = account.OrgId.ToString() }},
                { "filters", new AttributeValue { S = string.IsNullOrEmpty(filter) ? "ACCOUNT" : filter }}
            };
        }

        private static List<string> CreateFilters(Account account)
        {
            var filters = new List<string>
            {
                string.Empty,
                // type#creation_date
                $"#{account.CreatedAt:yyyy-MM-ddTHH:mm:ss}",
                // type#creation_date
                $"#{account.Type}#{account.CreatedAt:yyyy-MM-ddTHH:mm:ss}",
                // type#status#creation_date
                $"#{account.Type}#{account.Status}#{account.CreatedAt:yyyy-MM-ddTHH:mm:ss}",
                // type#ownership_type#creation_date
                $"#{account.Type}#{account.OwnershipType}#{account.CreatedAt:yyyy-MM-ddTHH:mm:ss}",
                // type#status#ownership_type#creation_date
                $"#{account.Type}#{account.Status}#{account.OwnershipType}#{account.CreatedAt:yyyy-MM-ddTHH:mm:ss}",
                // status#creation_date
                $"#{account.Status}#{account.CreatedAt:yyyy-MM-ddTHH:mm:ss}",
                // status#ownership_type#creation_date
                $"#{account.Status}#{account.OwnershipType}#{account.CreatedAt:yyyy-MM-ddTHH:mm:ss}",
                // ownership_type#creation_date
                $"#{account.OwnershipType}#{account.CreatedAt:yyyy-MM-ddTHH:mm:ss}"
            };

            return filters;
        }

        public Task InsertSubaccountAsync(Subaccount subaccount)
        {
            throw new NotImplementedException();
        }
    }
}
