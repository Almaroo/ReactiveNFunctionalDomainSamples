using LanguageExt;

namespace FRDomain_1.ValueObjects;

public record Money(Map<Currency, Amount> Pairs) : IComparable<Money>
{
    public Amount ToBaseCurrency => Pairs.Fold(Amount.Zero, (total, currency, next) =>
     total + next / (currency is Usd ? 1m : 4.5m)
    );

    public int CompareTo(Money? other)
    {
        if (ReferenceEquals(this, other)) return 0;
        if (ReferenceEquals(null, other)) return 1;
        return ToBaseCurrency.CompareTo(other.ToBaseCurrency);
    }
};
