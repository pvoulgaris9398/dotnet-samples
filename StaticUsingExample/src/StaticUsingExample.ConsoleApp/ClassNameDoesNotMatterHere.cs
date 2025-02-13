namespace StaticUsingExample.ConsoleApp
{
    internal sealed class ClassNameDoesNotMatterHere
    {
        internal static string GetValue(int arg1)
        {
            var t = typeof(ClassNameDoesNotMatterHere);
            LogMessage(t.ToString());
            LogMessage(nameof(GetValue));
            return $"The value is {arg1}";
        }
    }
}
