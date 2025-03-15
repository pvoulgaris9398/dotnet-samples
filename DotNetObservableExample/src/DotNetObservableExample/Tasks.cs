namespace DotNetObservableExample
{
    public static class Tasks
    {
        public static bool ShowMenu((Func<string>, Action)[] tasks)
        {
            Console.WriteLine(new string('*', 80));
            Console.WriteLine();
            Console.WriteLine("Please select from the following menu (Q to [Q]uit):");
            Console.WriteLine();
            Console.WriteLine(new string('*', 80));
            Console.WriteLine();
            var index = 0;
            foreach (var task in tasks)
            {
                index++;
                Console.WriteLine("[{0}]: {1}", index, task.Item1());
            }
            return true;
        }
    }
}
