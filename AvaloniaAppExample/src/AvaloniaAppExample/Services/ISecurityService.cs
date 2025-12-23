using AvaloniaAppExample.Models;

namespace AvaloniaAppExample.Services
{
    public interface ISecurityService
    {
        IObservable<IChangeSet<Security>> Securities { get; }
    }
}
