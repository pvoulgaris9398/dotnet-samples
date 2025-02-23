using AvaloniaAppExample.Models;
using DynamicData;
using System;

namespace AvaloniaAppExample.Services
{
    public interface IPriceService
    {
        IObservable<IChangeSet<Price>> Prices { get; }
    }
}
