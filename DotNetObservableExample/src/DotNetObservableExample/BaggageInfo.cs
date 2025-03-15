namespace DotNetObservableExample
{
    public readonly record struct BaggageInfo(
        int FlightNumber,
        string From,
        int Carousel);
}
