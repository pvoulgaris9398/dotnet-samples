using System.Reactive.Concurrency;

namespace DotNetObservableExample
{
    /*
     * https://www.zerobugbuild.com/?p=259
     * Need System.Reactive
     */
    public static class TestOne
    {
        public static IDisposable ScheduleRecurringAction(
    this IScheduler scheduler, TimeSpan interval, Action action)
        {
            return scheduler.Schedule(interval, scheduleNext =>
            {
                action();
                scheduleNext(interval);
            });
        }
        public static void Execute()
        {
            TimeSpan interval = TimeSpan.FromSeconds(5);
            Action work = () => Console.WriteLine("Doing some work...");

            var schedule =
                 Scheduler.Default.ScheduleRecurringAction(interval, work);

            Console.WriteLine("Press return to stop.");
            Console.ReadLine();
            schedule.Dispose();
        }
    }
}
