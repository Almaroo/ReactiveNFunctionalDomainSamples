using LanguageExt.TypeClasses;

namespace FRDomain_1.Monoids;

public struct AmountAdditionMonoid : Monoid<Amount>
{
    public static AmountAdditionMonoid Inst => new();
    
    public decimal Append(decimal x, decimal y) => x + y;

    public decimal Empty() => Decimal.Zero;
}
