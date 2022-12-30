namespace am.accounts.commons.Models
{
    using System;

    /// <summary>
    /// Defines an account key structure to retrieve information from the data store using this key.
    /// </summary>
    public struct AccountKey
    {
        /// <summary>
        /// The empty account key.
        /// </summary>

        private const string Id = "Id";
        private string keyValue;

        /// <summary>
        /// Gets the key value.
        /// </summary>
        public object KeyValue => this.keyValue;

        /// <summary>
        /// Gets the name of the key.
        /// </summary>
        public string KeyName { get; private set; }


        /// <summary>
        /// Creates a key for the field <see cref="Id"/>.
        /// </summary>
        /// <param name="accountId">The account identifier.</param>
        /// <param name="orgId">The organization identifier.</param>
        /// <returns>The account key.</returns>
        public static string CreateForIndexId(Guid orgId, Guid corpId, Guid accountId, string filter)
        {
            if (accountId == Guid.Empty)
            {
                throw new ArgumentException("Invalid AccountId Id", nameof(accountId));
            }

            if (orgId == Guid.Empty)
            {
                throw new ArgumentException("Invalid Organization Id", nameof(orgId));
            }


            if (corpId == Guid.Empty)
            {
                throw new ArgumentException("Invalid corpId Id", nameof(accountId));
            }


            var retval = default(AccountKey);
            retval.keyValue = $"{orgId}#{corpId}#{accountId}";
            retval.KeyName = Id;

            return retval.keyValue;
        }

        public override bool Equals(object obj)
        {
            if (obj is AccountKey sk)
            {
                var compare1 = this.KeyValue == sk.KeyValue;
                var compare2 = this.KeyName == sk.KeyName;

                return compare1 && compare2;
            }

            return false;
        }

        public static bool operator ==(AccountKey left, AccountKey right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(AccountKey left, AccountKey right)
        {
            return !(left == right);
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}
