using am.accounts.commons.Models;

namespace am.accounts.commons.Repository
{
    using Amazon.DynamoDBv2;
    using Amazon.DynamoDBv2.DataModel;
    using Amazon.DynamoDBv2.DocumentModel;
    using Amazon.DynamoDBv2.Model;
    using Amazon.Runtime.Internal.Transform;
    using System.Globalization;

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
                { "search_resource", new AttributeValue { S = string.IsNullOrEmpty(filter) ? "ACCOUNT" : $"ACCOUNT#{filter}" }},
                { "corporation_id", new AttributeValue { S = account.CorporationId.ToString() }},
                { "created_at", new AttributeValue { S = account.CreatedAt.ToString("yyyy-MM-dd'T'HH:mm:ss.fffK", CultureInfo.InvariantCulture) }},
                { "currency_code", new AttributeValue { S = account.CurrencyCode }},
                { "ending_in", new AttributeValue { S = account.EndingIn }},
                { "id", new AttributeValue { S = account.Id.ToString() }},
                { "org_id", new AttributeValue { S = account.OrgId.ToString() }},
                { "number", new AttributeValue { S = account.Number.ToString() }},
                { "external_id", new AttributeValue { S = account.ExternalId }},
                { "lorem_contract_id", new AttributeValue { S = account.LoremContractId.ToString() }},
                { "org_corp", new AttributeValue { S = account.OrgCorp.ToString() }},
                { "filters", new AttributeValue { S = string.IsNullOrEmpty(filter) ? "ACCOUNT" : filter }}
            };
        }

        private static List<string> CreateFilters(Account account)
        {
            var filters = new List<string>
            {
                string.Empty,
                // type#creation_date
                $"{account.CreatedAt:yyyy-MM-ddTHH:mm:ss}",
                // type#creation_date
                $"{account.Type}#{account.CreatedAt:yyyy-MM-ddTHH:mm:ss}",
                // type#status#creation_date
                $"{account.Type}#{account.Status}#{account.CreatedAt:yyyy-MM-ddTHH:mm:ss}",
                // type#ownership_type#creation_date
                $"{account.Type}#{account.OwnershipType}#{account.CreatedAt:yyyy-MM-ddTHH:mm:ss}",
                // type#status#ownership_type#creation_date
                $"{account.Type}#{account.Status}#{account.OwnershipType}#{account.CreatedAt:yyyy-MM-ddTHH:mm:ss}",
                // status#creation_date
                $"{account.Status}#{account.CreatedAt:yyyy-MM-ddTHH:mm:ss}",
                // status#ownership_type#creation_date
                $"{account.Status}#{account.OwnershipType}#{account.CreatedAt:yyyy-MM-ddTHH:mm:ss}",
                // ownership_type#creation_date
                $"{account.OwnershipType}#{account.CreatedAt.ToString("yyyy-MM-dd'T'HH:mm:ss.fffK", CultureInfo.InvariantCulture)}"
            };

            return filters;
        }

        public Task InsertSubaccountAsync(Subaccount subaccount)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAccountAsync(Account account)
        {
            var client = new AmazonDynamoDBClient();
            var context = new DynamoDBContext(client);
            var request = new TransactWriteItemsRequest();

            var queryResults = await client.QueryAsync(new QueryRequest
            {
                TableName = "am-accounts-test",
                KeyConditionExpression = "partition_key = :partition_key",
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>
                {
                    { ":partition_key", new AttributeValue { S = "e4c82a64-9bac-45a6-94ab-0af6352f01cb#2589e7c2-3f33-4d44-8714-fcfa11eded0e#5675def4-365b-4ce8-be44-6db134db087d"} }
                },
                ReturnConsumedCapacity = new ReturnConsumedCapacity("TOTAL"),
            });

            Console.WriteLine(queryResults.ConsumedCapacity.CapacityUnits);
            var writeRequest = new List<WriteRequest>();
            foreach (var item in queryResults.Items)
            {
                item.TryGetValue("partition_key", out var partitionKeyValue);
                item.TryGetValue("search_resource", out var searchResourceValue);

                writeRequest.Add(new WriteRequest
                {
                    DeleteRequest = new DeleteRequest
                    {
                        Key = new Dictionary<string, AttributeValue>
                        {
                            { "partition_key", partitionKeyValue },
                            { "search_resource", searchResourceValue }
                        },
                    }
                });
            }
            var result = await client.BatchWriteItemAsync(new Dictionary<string, List<WriteRequest>>
            {
                { "am-accounts-test", writeRequest},
            });

            foreach (var item in result.ConsumedCapacity)
            {
                Console.Write($"CU {item.CapacityUnits}");
            }
            var filters = CreateFilters(account);
            foreach (var f in filters)
            {
                request.TransactItems.Add(new TransactWriteItem
                {
                    Put = new Put
                    {
                        TableName = "am-accounts-test",
                        Item = AccountToDictionary(account, f)
                    }
                }); ;
            }
            var response = await client.TransactWriteItemsAsync(request);
            foreach (var item in response.ConsumedCapacity)
            {
                Console.Write($"CU {item.CapacityUnits}");
            }

            Console.Write(response);
        }
    }
}
