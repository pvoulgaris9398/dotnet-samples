namespace StaticUsingExample.Console
{
    internal class ClassNameDoesNotMatterHere
    {
        internal static string GetValue(int arg1)
        {
            Type t = typeof(ClassNameDoesNotMatterHere);
            WriteLine(t);
            WriteLine(nameof(GetValue));
            return $"The value is {arg1}";
        }
    }
}
