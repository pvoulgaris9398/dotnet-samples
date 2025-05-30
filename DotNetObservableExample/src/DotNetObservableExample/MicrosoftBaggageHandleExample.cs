﻿namespace DotNetObservableExample
{
    /*
    * https://learn.microsoft.com/en-us/dotnet/standard/events/observer-design-pattern
    */
    public static class MicrosoftBaggageHandleExample
    {
        public static string Description() => "Microsoft Baggage Handler Example";

        public static void Execute()
        {
            BaggageHandler provider = new();
            ArrivalsMonitor observer1 = new("BaggageClaimMonitor1");
            ArrivalsMonitor observer2 = new("SecurityExit");

            /*
             * "Something" (our Program.cs _main_ method here) updates the status
             * or triggers _an event_ in the _provider_
             */
            provider.BaggageStatus(712, "Detroit", 3);
            observer1.Subscribe(provider);

            provider.BaggageStatus(712, "Kalamazoo", 3);
            provider.BaggageStatus(400, "New York-Kennedy", 1);
            provider.BaggageStatus(712, "Detroit", 3);
            observer2.Subscribe(provider);

            provider.BaggageStatus(511, "San Francisco", 2);
            provider.BaggageStatus(712);
            observer2.Unsubscribe();

            provider.BaggageStatus(400);
            provider.LastBaggageClaimed();
        }
    }
}
