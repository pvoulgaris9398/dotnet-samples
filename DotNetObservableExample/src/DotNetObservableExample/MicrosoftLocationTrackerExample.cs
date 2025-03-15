namespace DotNetObservableExample
{
    /*
     * https://learn.microsoft.com/en-us/dotnet/api/system.iobservable-1?view=net-9.0
     */
    static class MicrosoftLocationTrackerExample
    {
        public static string Description => "Microsoft LocationTracker Sample";
        public static void Execute()
        {
            // Define a provider and two observers.
            LocationTracker provider = new LocationTracker();
            LocationReporter reporter1 = new LocationReporter("FixedGPS");
            reporter1.Subscribe(provider);
            LocationReporter reporter2 = new LocationReporter("MobileGPS");
            reporter2.Subscribe(provider);

            provider.TrackLocation(new Location(47.6456, -122.1312));
            reporter1.Unsubscribe();
            provider.TrackLocation(new Location(47.6677, -122.1199));
            provider.TrackLocation(null);
            provider.EndTransmission();
        }
    }

    public struct Location
    {
        double lat, lon;

        public Location(double latitude, double longitude)
        {
            this.lat = latitude;
            this.lon = longitude;
        }

        public double Latitude
        { get { return this.lat; } }

        public double Longitude
        { get { return this.lon; } }
    }

    public class LocationTracker : IObservable<Location>
    {
        public LocationTracker()
        {
            observers = new List<IObserver<Location>>();
        }

        private List<IObserver<Location>> observers;

        public IDisposable Subscribe(IObserver<Location> observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);
            return new Unsubscriber(observers, observer);
        }

        private class Unsubscriber : IDisposable
        {
            private List<IObserver<Location>> _observers;
            private IObserver<Location> _observer;

            public Unsubscriber(List<IObserver<Location>> observers, IObserver<Location> observer)
            {
                this._observers = observers;
                this._observer = observer;
            }

            public void Dispose()
            {
                if (_observer != null && _observers.Contains(_observer))
                    _observers.Remove(_observer);
            }
        }

        public void TrackLocation(Nullable<Location> loc)
        {
            foreach (var observer in observers)
            {
                if (!loc.HasValue)
                    observer.OnError(new LocationUnknownException());
                else
                    observer.OnNext(loc.Value);
            }
        }

        public void EndTransmission()
        {
            foreach (var observer in observers.ToArray())
                if (observers.Contains(observer))
                    observer.OnCompleted();

            observers.Clear();
        }
    }

    public class LocationUnknownException : Exception
    {
        internal LocationUnknownException()
        { }
    }

    public class LocationReporter : IObserver<Location>
    {
        private IDisposable? unsubscriber;
        private string instName;

        public LocationReporter(string name)
        {
            this.instName = name;
        }

        public string Name
        { get { return this.instName; } }

        public virtual void Subscribe(IObservable<Location> provider)
        {
            if (provider != null)
                unsubscriber = provider.Subscribe(this);
        }

        public virtual void OnCompleted()
        {
            Console.WriteLine("The Location Tracker has completed transmitting data to {0}.", this.Name);
            this.Unsubscribe();
        }

        public virtual void OnError(Exception e)
        {
            Console.WriteLine("{0}: The location cannot be determined.", this.Name);
        }

        public virtual void OnNext(Location value)
        {
            Console.WriteLine("{2}: The current location is {0}, {1}", value.Latitude, value.Longitude, this.Name);
        }

        public virtual void Unsubscribe()
        {
            unsubscriber?.Dispose();
        }
    }
}
