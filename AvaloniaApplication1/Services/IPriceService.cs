using AvaloniaApplication1.Models;
using DynamicData;
using System;

namespace AvaloniaApplication1.Services
{
    public interface IPriceService
    {
        IObservable<IChangeSet<Price>> Prices { get; }
    }
}
