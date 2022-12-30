namespace am.accounts.commons.Models
{
    public class Account
    {
        /// <summary>
        /// Gets or sets the partition key.
        /// </summary>
        public string? PartitionKey { get; set; }

        /// <summary>
        /// Gets or sets the resource.
        /// </summary>
        public string? SearchResource { get; set; }

        /// <summary>
        /// Gets or sets the org corp.
        /// </summary>
        public string? OrgCorp { get; set; }
        
        /// <summary>
        /// Gets or sets the corporation identifier.
        /// </summary>
        public Guid CorporationId { get; set; }

        /// <summary>
        /// Gets or sets the created at.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the currency code.
        /// </summary>
        public string? CurrencyCode { get; set; }
        /// <summary>
        /// Gets or sets the ending in.
        /// </summary>
        public string? EndingIn { get; set; }

        /// <summary>
        /// Gets or sets the number.
        /// </summary>
        public Guid Number { get; set; }

        /// <summary>
        /// Gets or sets the external identifier.
        /// </summary>
        public string? ExternalId { get; set; }

        /// <summary>
        /// Gets or sets the lorem contract identifier.
        /// </summary>
        public Guid LoremContractId { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the org identifier.
        /// </summary>
        public Guid OrgId { get; set; }

        /// <summary>
        /// Gets or sets the filter.
        /// </summary>
        public string? Filter { get; set; }

        public string Type { get; set; }

        public string Status { get; set; }

        public string OwnershipType { get; set; }
    }
}
