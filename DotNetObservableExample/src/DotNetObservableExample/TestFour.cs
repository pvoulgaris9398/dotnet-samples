using System.Reactive.Concurrency;
using System.Reactive.Linq;

namespace DotNetObservableExample
{
    static class TestFour
    {
        public static void Execute()
        {
            using (var observer = Observable.Start(() => 1, Scheduler.CurrentThread).Subscribe())
            {
                Console.WriteLine(observer);
            }
        }
    }
}
