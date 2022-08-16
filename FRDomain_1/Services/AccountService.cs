using FRDomain_1.Aggregates;
using FRDomain_1.Exceptions;
using FRDomain_1.Repositories;
using LanguageExt;

namespace FRDomain_1.Services;

public class AccountService : IAccountService
{
    public Reader<IAccountRepository, Try<Amount>> Balance(Guid id) =>
        accountRepository => ReaderResult<Try<Amount>>.New(() => 
            accountRepository.Balance(id).Match(
                x => x,
                exception => throw new Exception(exception.Message)
                )
        );

    public Reader<IAccountRepository, Try<Account>> Deposit(Guid id, decimal amount) =>
        accountRepository => ReaderResult<Try<Account>>.New(() =>
            accountRepository.Get(id).Match(
                account =>
                {
                    var newAccount = Account.New(account.Id, account.Amount + amount);
                    
                    return accountRepository.Store(newAccount).Try();
                },
                () => throw new AccountNotFoundException("Account not found"),
                exception => throw new Exception(exception.Message))
        );

    public Reader<IAccountRepository, Try<Account>> Credit(Guid id, decimal amount) =>
        accountRepository => ReaderResult<Try<Account>>.New(() =>
            accountRepository.Get(id).Match(
                account =>
                {
                    var newAccount = account.Amount >= amount
                        ? Account.New(account.Id, account.Amount - amount)
                        : throw new InsufficientFundsException("Insufficient funds");

                    return accountRepository.Store(newAccount).Try();
                },
                () => throw new AccountNotFoundException("Account not found"),
                exception => throw new Exception(exception.Message))
        );

    public Reader<IAccountRepository, Try<Account>> Open(Guid id) =>
        accountRepository => ReaderResult<Try<Account>>.New(() =>
            accountRepository.Get(id).Match(
                _ => throw new AccountAlreadyExistsException("Account already exists"),
                () =>
                {
                    var account = Account.New(id, Decimal.Zero); 
                    return accountRepository.Store(account).Try();
                },
                exception => throw new Exception(exception.Message)
            ));
}
