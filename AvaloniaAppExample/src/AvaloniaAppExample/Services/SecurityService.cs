using System;
using System.Linq;
using AvaloniaAppExample.Models;
using DynamicData;

namespace AvaloniaAppExample.Services
{
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