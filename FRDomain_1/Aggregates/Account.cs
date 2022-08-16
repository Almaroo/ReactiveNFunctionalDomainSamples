namespace FRDomain_1.Aggregates;

public class Account
{
    private Account() {}
    
    public Guid Id { get; set; }
    public Amount Amount { get; set; }

    public static Account New() => new() { Id = Guid.NewGuid(), Amount = Decimal.Zero };
    public static Account New(Guid id, Amount amount) => new() { Id = id, Amount = amount };

    public override string ToString()
    {
        return $"Account [{Id}] with balance: {Amount}";
    }
}
