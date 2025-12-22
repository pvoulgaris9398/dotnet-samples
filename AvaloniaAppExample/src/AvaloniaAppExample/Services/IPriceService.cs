namespace AvaloniaAppExample.Services
{
    public interface IPriceService
    {
        IObservable<IChangeSet<Price>> Prices { get; }
    }
}
