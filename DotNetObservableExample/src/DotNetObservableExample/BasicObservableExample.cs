using System.Reactive.Concurrency;
using System.Reactive.Linq;

namespace DotNetObservableExample
{
    static class BasicObservableExample
    {
        public static string Description() => "Basic Observable Example";
        public static void Execute()
        {
            using (var observer = Observable.Start(() => 1, Scheduler.CurrentThread).Subscribe())
            {
                Console.WriteLine(observer);
            }
        }
    }
}
