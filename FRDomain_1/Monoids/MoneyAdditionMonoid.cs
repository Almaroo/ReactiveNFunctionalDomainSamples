using FRDomain_1.ValueObjects;
using LanguageExt;
using LanguageExt.TypeClasses;
using static LanguageExt.TypeClass;

namespace FRDomain_1.Monoids;

public class MoneyAdditionMonoid : Monoid<Money>
{
    public static MoneyAdditionMonoid Inst => new();
    
    public Money Append(Money x, Money y) =>
       new(mconcat<MoneyPairsMonoid, Map<Currency, decimal>>(x.Pairs, y.Pairs));

    public Money Empty() => new(Map<Currency, decimal>.Empty);
}
