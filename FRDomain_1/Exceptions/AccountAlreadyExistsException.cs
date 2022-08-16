namespace FRDomain_1.Exceptions;

public class AccountAlreadyExistsException : AccountException
{
    public AccountAlreadyExistsException(string? message) : base(message) { }

    public AccountAlreadyExistsException(string? message, Exception? innerException) : base(message, innerException) { }
}
