namespace LibraryOne
{
    public class ClassNameDoesNotMatterHereEither
    {
        public static string GetValue(int arg1)
        {
            Type t = typeof(ClassNameDoesNotMatterHereEither);
            WriteLine(t);
            WriteLine(nameof(GetValue));
            return $"The value is {arg1}";
        }
    }
}
