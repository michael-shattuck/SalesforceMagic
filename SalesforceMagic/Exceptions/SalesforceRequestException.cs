using System;

namespace SalesforceMagic.Exceptions
{
    public class SalesforceRequestException : Exception
    {
        public SalesforceRequestException()
        {
        }

        public SalesforceRequestException(string message) : base(message)
        {
        }

        public SalesforceRequestException(string message, params object[] args)
            : base(string.Format(message, args))
        {
        }
    }
}