using AvaloniaApplication1.Models;
using DynamicData;
using System;
using System.Linq;

namespace AvaloniaApplication1.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly SourceList<Security> _items = new();

        public SecurityService()
        {
            _items.AddRange(Enumerable.Range(1, 20).Select(i => Security.Create(i)));
        }

        public IObservable<IChangeSet<Security>> Securities => _items.Connect();
    }
}