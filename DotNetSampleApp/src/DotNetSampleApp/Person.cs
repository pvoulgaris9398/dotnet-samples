namespace DotNetSampleApp
{
    public abstract class Foo
    {
        public abstract void Run();
    }

    public class Person : Foo
    {
        public static void Weekday(DayOfWeek day)
        {
            if (day.ToString() == thisIsAConstant)
            {
                Console.WriteLine("We are here!");
            }
            return;
        }

        public const string thisIsAConstant = "1";

        /// <summary>
        /// IDE0036
        /// https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/style-rules/ide0036
        /// </summary>
        public override async void Run()
        {
            try
            {
                Console.WriteLine(thisIsAConstant);
                await Task.Run(() => Thread.Sleep(1));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
