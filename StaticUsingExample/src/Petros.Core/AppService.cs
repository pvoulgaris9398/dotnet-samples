namespace Petros.Core
{
    public static class AppService
    {
        internal static Action<string> DefaultLogCallback { get; set; } =
            msg =>
            {
                var currentColor = ForegroundColor;
                try
                {
                    ForegroundColor = ConsoleColor.Green;
                    WriteLine(msg);
                }
                finally
                {
                    ForegroundColor = currentColor;
                }
            };

        internal static Action<string> LogCallback { get; set; } = DefaultLogCallback;

        public class LogMethod
        {
            public static void Set(Action<string> logCallback)
            {
                LogCallback = logCallback;
            }

            public static void Reset()
            {
                LogCallback = DefaultLogCallback;
            }
        }

        public static void LogMessage(string message)
        {
            LogCallback(message);
        }
    }
}
