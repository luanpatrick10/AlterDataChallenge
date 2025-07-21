using Shared.Exceptions;

namespace Shared.Validations;

public static class Validations
{
    
    
    public static void IsNotNullOrEmpty(string value)
    {
        IsNotNull(value);
        if(value.Trim().Length == 0)
            throw new DomainException("Value cannot be empty.");
    }
    public static void IsNotNull(dynamic value)
    {
        if(value == null)
            throw new DomainException("Value cannot be null.");
    }
    
    
}