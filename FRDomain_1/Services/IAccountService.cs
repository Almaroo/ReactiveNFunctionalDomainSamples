using FRDomain_1.Aggregates;
using FRDomain_1.Repositories;
using LanguageExt;

namespace FRDomain_1.Services;

public interface IAccountService
{
    Reader<IAccountRepository, Try<Amount>> Balance(Guid id);
    Reader<IAccountRepository, Try<Account>> Deposit(Guid id, Amount amount);
    Reader<IAccountRepository, Try<Account>> Credit(Guid id, Amount amount);
    Reader<IAccountRepository, Try<Account>> Open(Guid id);
}
