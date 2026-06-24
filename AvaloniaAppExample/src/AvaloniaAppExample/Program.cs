using System.Runtime.InteropServices;

namespace AvaloniaAppExample
{
    internal sealed class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        [STAThread]
        public static void Main(string[] args)
        {
            Console.WriteLine(RuntimeInformation.FrameworkDescription);

            Console.WriteLine(Environment.Version);

            Console.WriteLine(Environment.Is64BitProcess);

            _ = BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
        }

        // Avalonia configuration, don't remove; also used by visual designer.
        // csharpier-ignore
        public static AppBuilder BuildAvaloniaApp() =>
            AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .UseReactiveUI(_ => { })
            .LogToTrace();
    }
}
