using AvaloniaAppExample.Models;
using DynamicData;
using System;
using System.Linq;

namespace AvaloniaAppExample.Services
{
    public class SecurityService : ISecurityService
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
    }
}