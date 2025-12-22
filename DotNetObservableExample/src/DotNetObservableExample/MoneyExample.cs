namespace DotNetObservableExample
{
    public static class MoneyExample
    {
        public static string Description() => "Money Example";

        public static void Execute()
        {
            int[] numbers = [1, 2, 3, 4, 5, 6, 7, 8, 9];

            var sum = numbers.Aggregate(0, (acc, x) => acc + x);

            Console.WriteLine("Sum: {0}", sum);
        }

        public static IMoney? TryCreate(decimal amount, Currency currency) =>
            amount < 0 ? null
            : amount == 0 ? NoMoney.Value
            : currency == Currency.Empty ? null
            : new Money(amount, currency);
    }

    public class NoMoney : IMoney
    {
#pragma warning disable CA2211 // Non-constant fields should not be visible
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public static Money Value;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
#pragma warning restore CA2211 // Non-constant fields should not be visible
        public decimal Amount => throw new NotImplementedException();

        public Currency Currency => throw new NotImplementedException();
    }

    public class Money : IMoney
    {
#pragma warning disable IDE0290 // Use primary constructor
        public Money(decimal amount, Currency currency)
#pragma warning restore IDE0290 // Use primary constructor
        {
            Amount = Math.Round(amount, 2);
            Currency = currency;
        }

        public decimal Amount { get; }

        public Currency Currency { get; }
    }

    public interface IMoney
    {
        decimal Amount { get; }
        Currency Currency { get; }
    }

    public class Currency
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public static Currency Empty { get; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    }
}
