namespace LibraryOne
{
    public class ClassNameDoesNotMatterHereEither
    {
        public static string GetValue(int arg1, Action<string> log)
        {
            var t = typeof(ClassNameDoesNotMatterHereEither);
            log(t.ToString());
            log(nameof(GetValue));
            return $"The value is {arg1}";
        }
    }
}
