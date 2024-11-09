namespace StaticUsingExample.Console
{
    internal class ClassNameDoesNotMatterHere
    {
        internal static string GetValue(int arg1, Action<string> log)
        {
            Type t = typeof(ClassNameDoesNotMatterHere);
            log(t.ToString());
            log(nameof(GetValue));
            return $"The value is {arg1}";
        }
    }
}
