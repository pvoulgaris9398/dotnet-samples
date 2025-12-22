namespace DotNetObservableExample
{
    public class ArrivalsMonitor : IObserver<BaggageInfo>
    {
        private readonly string _name;
        private readonly List<string> _flights = [];
        private IDisposable? _cancellation;

        public ArrivalsMonitor(string name)
        {
            ArgumentException.ThrowIfNullOrEmpty(name);
            _name = name;
        }

        public virtual void Subscribe(BaggageHandler provider)
        {
            _cancellation = provider.Subscribe(this);
        }

        public virtual void Unsubscribe()
        {
            _cancellation?.Dispose();
            _flights.Clear();
        }

        public virtual void OnCompleted()
        {
            _flights.Clear();
        }

        // No implementation needed: Method is not called by the BaggageHandler class.

        // Update information.
        public virtual void OnNext(BaggageInfo info)
        {
            bool updated = false;

            // Flight has unloaded its baggage; remove from the monitor.
            if (info.Carousel is 0)
            {
                string flightNumber = $"{info.FlightNumber, 5}";
                for (int index = _flights.Count - 1; index >= 0; index--)
                {
                    string flightInfo = _flights[index];
                    if (flightInfo.Substring(21, 5).Equals(flightNumber, StringComparison.Ordinal))
                    {
                        updated = true;
                        _flights.RemoveAt(index);
                    }
                }
            }
            else
            {
                // Add flight if it doesn't exist in the collection.
                string flightInfo = $"{info.From} {info.FlightNumber} {info}";
                if (!_flights.Contains(flightInfo))
                {
                    _flights.Add(flightInfo);
                    updated = true;
                }
            }

            if (updated)
            {
                _flights.Sort();
                Console.WriteLine($"Arrivals information from {_name}");
                foreach (string flightInfo in _flights)
                {
                    Console.WriteLine(flightInfo);
                }

                Console.WriteLine();
            }
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }
    }
}
