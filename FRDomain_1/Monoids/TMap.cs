using FRDomain_1.ValueObjects;
using LanguageExt;
using LanguageExt.ClassInstances;
using LanguageExt.TypeClasses;

namespace FRDomain_1.Monoids;

// ReSharper disable once InconsistentNaming
// I hope it can be more generic than this...
public struct MoneyPairsMonoid : Monoid<Map<Currency, decimal>>
{
    public static MoneyPairsMonoid Inst = new();
    
    public Map<Currency, decimal> Append(Map<Currency, decimal> m1, Map<Currency, decimal> m2) =>
        m2.Fold(m1, (map, key, value) =>
            map
                .Find(key)
                .Map(v => map.AddOrUpdate(key, TDecimal.Inst.Append(v, value)))
                .IfNone(map.AddOrUpdate(key, value))
            );

    public Map<Currency, decimal> Empty() => Map<Currency, decimal>.Empty;
}
