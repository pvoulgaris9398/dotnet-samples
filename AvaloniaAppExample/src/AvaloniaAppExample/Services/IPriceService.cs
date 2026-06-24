using AvaloniaAppExample.Infrastructure;
using AvaloniaAppExample.Models;

namespace AvaloniaAppExample.Services
{
    public interface IPriceService
    {
        IObservable<IChangeSet<Price, string>> Prices { get; }

        IObservable<DashboardTelemetrySnapshot> TelemetryUpdates { get; }
    }
}
