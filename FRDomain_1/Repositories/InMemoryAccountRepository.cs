using FRDomain_1.Aggregates;
using FRDomain_1.Exceptions;
using LanguageExt;
using static LanguageExt.Prelude;

namespace FRDomain_1.Repositories;

public class InMemoryAccountRepository : IAccountRepository
{
    private Map<Guid, Account> _accounts;

    public TryOption<Account> Get(Guid id) =>
        () =>
        {
            ArgumentNullException.ThrowIfNull(id);
            return _accounts.Find(id);
        };

    public Try<Account> Store(Account aggregate) =>
        () =>
        {
            ArgumentNullException.ThrowIfNull(aggregate);
            _accounts = _accounts.AddOrUpdate(aggregate.Id, aggregate);
            return _accounts[aggregate.Id];
        };

    public Try<decimal> Balance(Guid id) =>
        () => Get(id).Match(
            account => account.Amount,
            () => throw new AccountNotFoundException("Account was not found"),
            exception => throw new Exception(exception.Message)
        );
}
