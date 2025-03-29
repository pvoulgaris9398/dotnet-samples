namespace DotNetObservableExample
{
    public sealed class BaggageHandler : IObservable<BaggageInfo>
    {
        private readonly HashSet<IObserver<BaggageInfo>> _observers = [];
        private readonly HashSet<BaggageInfo> _flights = [];
        public IDisposable Subscribe(IObserver<BaggageInfo> observer)
        {

            if (_observers.Add(observer))
            {
                foreach (BaggageInfo item in _flights)
                {
                    observer.OnNext(item);
                }
            }
            return new Unsubscriber<BaggageInfo>(_observers, observer);
        }
        // Called to indicate all baggage is now unloaded.
        public void BaggageStatus(int flightNumber) =>
            BaggageStatus(flightNumber, string.Empty, 0);

        public void BaggageStatus(int flightNumber, string from, int carousel)
        {
            var info = new BaggageInfo(flightNumber, from, carousel);

            // Carousel is assigned, so add new info object to list.
            if (carousel > 0 && _flights.Add(info))
            {
                foreach (IObserver<BaggageInfo> observer in _observers)
                {
                    observer.OnNext(info);
                }
            }
            else if (carousel is 0)
            {
                // Baggage claim for flight is done.
                if (_flights.RemoveWhere(
                    flight => flight.FlightNumber == info.FlightNumber) > 0)
                {
                    foreach (IObserver<BaggageInfo> observer in _observers)
                    {
                        observer.OnNext(info);
                    }
                }
            }
        }

        public void LastBaggageClaimed()
        {
            foreach (IObserver<BaggageInfo> observer in _observers)
            {
                observer.OnCompleted();
            }

            _observers.Clear();
        }

    }
}
