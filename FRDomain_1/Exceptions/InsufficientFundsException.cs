namespace FRDomain_1.Exceptions;

public class InsufficientFundsException : AccountException
{
    public InsufficientFundsException(string? message) : base(message) { }

    public InsufficientFundsException(string? message, Exception? innerException) : base(message, innerException) { }
}
