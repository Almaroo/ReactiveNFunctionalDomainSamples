namespace FRDomain_1.Exceptions;

public abstract class AccountException : Exception
{
    protected AccountException(string? message) : base(message) { }

    protected AccountException(string? message, Exception? innerException) : base(message, innerException) { }
}
