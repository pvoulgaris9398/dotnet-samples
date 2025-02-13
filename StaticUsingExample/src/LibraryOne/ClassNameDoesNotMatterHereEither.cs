namespace LibraryOne
{
    public class ClassNameDoesNotMatterHereEither
    {
        public static string GetValue(int arg1)
        {
            var t = typeof(ClassNameDoesNotMatterHereEither);
            LogMessage(t.ToString());
            LogMessage(nameof(GetValue));
            return $"The value is {arg1}";
        }
    }
}
