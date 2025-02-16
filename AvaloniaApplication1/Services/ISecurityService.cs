using AvaloniaApplication1.Models;
using DynamicData;
using System;

namespace AvaloniaApplication1.Services
{
    public interface ISecurityService
    {
        IObservable<IChangeSet<Security>> Securities { get; }
    }
}
