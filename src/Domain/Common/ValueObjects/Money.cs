using Domain.Common;
namespace Domain.Common.ValueObjects;
public record Money(decimal Amount, CurrencyCode Currency)
{
    public bool IsFree() => Amount == 0;
};
