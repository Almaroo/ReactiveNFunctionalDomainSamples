using FRDomain_1.Aggregates;
using LanguageExt;

namespace FRDomain_1.Repositories;

public interface IRepository<TAggregate, TId>
{
    TryOption<Account> Get(TId id);
    Try<TAggregate> Store(TAggregate aggregate);
}
