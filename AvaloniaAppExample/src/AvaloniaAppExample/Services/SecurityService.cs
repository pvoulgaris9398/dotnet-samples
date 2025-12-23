using AvaloniaAppExample.Models;

namespace AvaloniaAppExample.Services
{
    // Ensure the Security type is defined and accessible in this namespace or imported via using.
    // If Security is defined elsewhere, add the appropriate using directive.
    // using AvaloniaAppExample.Models; // Example if Security is in Models namespace

    public class SecurityService : ISecurityService, IDisposable
    {
        private readonly SourceList<Security> _items = new();

        #region Hacky - Just experimenting here

        public void AddItem() => _items.Add(Security.Create(_items.Count + 1));

        public void RemoveItem()
        {
            if (_items.Count > 0)
            {
                _items.RemoveAt(0);
            }
        }

        #endregion

        public SecurityService()
        {
            _items.AddRange(Enumerable.Range(1, 4).Select(i => Security.Create(i)));
        }

        public IObservable<IChangeSet<Security>> Securities => _items.Connect();

        public void Dispose()
        {
            _items.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
