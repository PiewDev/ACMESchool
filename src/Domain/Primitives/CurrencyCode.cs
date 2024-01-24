using System.ComponentModel;

namespace Domain.Primitives;
public enum CurrencyCode
{
    [Description("Euros")]
    EUR,
    [Description("Pesos Argentinos")]
    ARS,
}