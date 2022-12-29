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
        public string? Resource { get; set; }

        /// <summary>
        /// Gets or sets the corporation identifier.
        /// </summary>
        public Guid CorporationId { get; set; }

        /// <summary>
        /// Gets or sets the created at.
        /// </summary>
        /// <value>
        /// The created at.
        /// </value>
        public DateTime CreatedAt { get; set; }

        public string? CurrencyCode { get; set; }

        public string? EndingIn { get; set; }

        public Guid Id { get; set; }

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
