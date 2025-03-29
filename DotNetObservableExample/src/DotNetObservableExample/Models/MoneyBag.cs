namespace DotNetObservableExample.Models
{
    public record MoneyBag(decimal Amount)
    {
        public static MoneyBag Empty => new(0);
        public MoneyBag Add(decimal amount) => throw new NotImplementedException();
        public MoneyBag Subtract(decimal amount) => throw new NotImplementedException();
    }
}
