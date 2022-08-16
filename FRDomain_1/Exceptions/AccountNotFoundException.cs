namespace FRDomain_1.Exceptions;

public class AccountNotFoundException : AccountException
{
    public AccountNotFoundException(string? message) : base(message) { }

    public AccountNotFoundException(string? message, Exception? innerException) : base(message, innerException) { }
}
