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
        public static Money Value;
        public decimal Amount => throw new NotImplementedException();

        public Currency Currency => throw new NotImplementedException();
    }

    public class Money : IMoney
    {
        public Money(decimal amount, Currency currency)
        {
            Amount = Math.Round(amount, 2);
        }
        public decimal Amount { get; }

        public Currency Currency { get; }
    }


    public interface IMoney
    {
        decimal Amount { get; }
        public Currency Currency { get; }
    }

    public class Currency
    {
        public static Currency Empty { get; }
    }
}
