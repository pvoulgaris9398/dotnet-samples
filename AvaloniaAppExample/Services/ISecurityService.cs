using AvaloniaAppExample.Models;
using DynamicData;
using System;

namespace AvaloniaAppExample.Services
{
    public interface ISecurityService
    {
        IObservable<IChangeSet<Security>> Securities { get; }
    }
}
