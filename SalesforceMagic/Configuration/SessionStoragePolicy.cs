using System;

namespace SalesforceMagic.Configuration
{
    public class SessionStoragePolicy
    {
        public SessionStoragePolicy(TimeSpan sessionStorageExpiration)
        {
            SessionStorageExpiration = sessionStorageExpiration;
        }

        public TimeSpan SessionStorageExpiration { get; set; }
        public string StorageKeyPrefix { get; set; }
    }
}