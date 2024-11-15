namespace StaticUsingExample.Console
{
    internal sealed class ClassNameDoesNotMatterHere
    {
        internal static string GetValue(int arg1, Action<string> log)
        {
            var t = typeof(ClassNameDoesNotMatterHere);
            log(t.ToString());
            log(nameof(GetValue));
            return $"The value is {arg1}";
        }
    }
}
