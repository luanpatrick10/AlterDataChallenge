using System;

namespace Shared.Exceptions
{
    public class BusinessRuleException : Exception
    {
        public BusinessRuleException(string message = "Business rule violation occurred.") : base(message)
        {
        }
    }
}
