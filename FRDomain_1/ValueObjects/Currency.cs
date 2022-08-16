namespace FRDomain_1.ValueObjects;

public abstract record Currency() : IComparable<Currency>
{
    public int CompareTo(Currency? other) => string.Compare(GetType().Name, other?.GetType().Name, StringComparison.Ordinal);
}

public record Usd : Currency { }

public record Pln : Currency { }
