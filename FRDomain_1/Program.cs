using System.Text.Json;
using System.Text.Json.Nodes;
using FRDomain_1.Monoids;
using FRDomain_1.Repositories;
using FRDomain_1.Services;
using FRDomain_1.ValueObjects;
using LanguageExt;
using static LanguageExt.Prelude;

namespace FRDomain_1;

public class Program
{ 
    static AccountService App { get; } = new();
    private static InMemoryAccountRepository Repository = new();

    public static void Main()
    {
        var id = new Guid("E99C14C4-9747-4751-BBE8-76A8D4BF5027");

        var open = from _1 in App.Open(id) select _1;

        open
            .Run(Repository)
            .Match(
                result =>
                    result.Match(
                        Succ: Console.WriteLine,
                        Fail: exception => Console.WriteLine(exception.Message)),
                error => Console.WriteLine($"Error occured: {error.Message}")
            );

        var balance =
            from result in App.Balance(id)
            select result;

        balance
            .Run(Repository)
            .Match(
                result =>
                    result.Match(
                        balance => Console.WriteLine($"Balance for account [{id}] is {balance:C}"),
                        exception => Console.WriteLine(exception.Message)),
                error => Console.WriteLine($"Error occured: {error.Message}")
            );

        var deposit250 =
            from result in App.Deposit(id, 250m)
            select result;

        deposit250
            .Run(Repository)
            .Match(
                result =>
                    result.Match(
                        account => Console.WriteLine(account),
                        exception => Console.WriteLine(exception.Message)),
                error => Console.WriteLine($"Error occured: {error.Message}")
            );

        balance
            .Run(Repository)
            .Match(
                result =>
                    result.Match(
                        balance => Console.WriteLine($"Balance for account [{id}] is {balance:C}"),
                        exception => Console.WriteLine(exception.Message)),
                error => Console.WriteLine($"Error occured: {error.Message}")
            );

        deposit250
            .Run(Repository)
            .Match(
                result =>
                    result.Match(
                        account => Console.WriteLine(account),
                        exception => Console.WriteLine(exception.Message)),
                error => Console.WriteLine($"Error occured: {error.Message}")
            );

        balance
            .Run(Repository)
            .Match(
                result =>
                    result.Match(
                        balance => Console.WriteLine($"Balance for account [{id}] is {balance:C}"),
                        exception => Console.WriteLine(exception.Message)),
                error => Console.WriteLine($"Error occured: {error.Message}")
            );

        var credit300 =
            from result in App.Credit(id, 300m)
            select result;

        credit300
            .Run(Repository)
            .Match(
                result =>
                    result.Match(
                        account => Console.WriteLine(account),
                        exception => Console.WriteLine(exception.Message)),
                error => Console.WriteLine($"Error occured: {error.Message}")
            );

        balance
            .Run(Repository)
            .Match(
                result =>
                    result.Match(
                        balance => Console.WriteLine($"Balance for account [{id}] is {balance:C}"),
                        exception => Console.WriteLine(exception.Message)),
                error => Console.WriteLine($"Error occured: {error.Message}")
            );

        credit300
            .Run(Repository)
            .Match(
                result =>
                    result.Match(
                        account => Console.WriteLine(account),
                        exception => Console.WriteLine(exception.Message)),
                error => Console.WriteLine($"Error occured: {error.Message}")
            );

        var deposit500 =
            from _1 in App.Deposit(id, 250m)
            from _2 in App.Deposit(id, 250m)
            select _1 + _2;

        deposit500
            .Run(Repository)
            .Match(
                result =>
                    result.Match(
                        account => Console.WriteLine(account),
                        exception => Console.WriteLine(exception.Message)),
                error => Console.WriteLine($"Error occured: {error.Message}")
            );

        var deposit200 =
            from _1 in App.Deposit(id, 250m)
            from _2 in App.Deposit(id, 250m)
            from _3 in App.Credit(id, 300m)
            select _1 + _2 + _3;

        deposit200
            .Run(Repository)
            .Match(
                result =>
                    result.Match(
                        account => Console.WriteLine(account),
                        exception => Console.WriteLine(exception.Message)),
                error => Console.WriteLine($"Error occured: {error.Message}")
            );


        var m1 = new Money(Map<Currency, decimal>((new Usd(), 500m), (new Pln(), 100m)));

        var m2 = new Money(Map<Currency, decimal>((new Pln(), 500m)));

        var m3 = MoneyAdditionMonoid.Inst.Append(m1, m2);

        Console.WriteLine(string.Join(", ",
            m3.Pairs.Select((currency, value) => $"[{currency.GetType().Name}] : [{value}]")));

        var o1 = new JsonObject(new[]
        {
            new KeyValuePair<string, JsonNode?>("a", JsonValue.Create("valueA")),
            new KeyValuePair<string, JsonNode?>("b", JsonValue.Create("valueB")),
        });

        var o2 = new JsonObject(new[]
        {
            new KeyValuePair<string, JsonNode?>("b", JsonValue.Create("overridenValueB")),
        });

        var o3 = o1.AddOrReplace(o2);

        var o4 = new JsonObject(new[]
        {
            new KeyValuePair<string, JsonNode?>("a", new JsonObject(new []
                {
                    new KeyValuePair<string, JsonNode?>("subA_A", JsonValue.Create("nested A")),
                })),
        });

        o3 = o3.AddOrReplace(o4);
        
        var o5 = new JsonObject(new[]
        {
            new KeyValuePair<string, JsonNode?>("a", new JsonObject(new []
            {
                new KeyValuePair<string, JsonNode?>("subA_B", JsonValue.Create("nested B")),
            })),
        });

        var o6 = o3.AddOrReplace(o5);
        var o7 = o3.AddOrAppend(o5);

        var o8 = new JsonObject().AddOrReplace(o7);
        var o9 = o7.AddOrReplace(new JsonObject());
    }
}
