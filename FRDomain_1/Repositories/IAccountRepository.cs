using FRDomain_1.Aggregates;
using LanguageExt;

namespace FRDomain_1.Repositories;

public interface IAccountRepository : IRepository<Account, Guid>
{
    Try<Amount> Balance(Guid id);
}
