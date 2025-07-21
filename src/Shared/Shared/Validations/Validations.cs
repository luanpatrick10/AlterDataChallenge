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

    public static void DateIsNotNull(DateTime? date)
    {
        if(date == null || date.Value == DateTime.MinValue)
            throw new DomainException("Value cannot be null.");
    }
    
    public static void DateIsGreaterThan(DateTime? date, DateTime minDate)
    {
        DateIsNotNull(date);
        if (date!.Value < minDate)
            throw new DomainException($"Date must be greater than {minDate}.");
    }

    public static void TimeIsNotNegative(TimeSpan? time)
    {
        if (time < TimeSpan.Zero)
            throw new ArgumentException("Time spent cannot be negative", nameof(time));
    }
}