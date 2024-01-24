using Domain.Primitives;
namespace Domain.ValueObjects;
public record Money(decimal Amount, CurrencyCode Currency);
