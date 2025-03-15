using System.Reactive.Linq;

namespace DotNetObservableExample
{
    /*
     *  https://learn.microsoft.com/en-us/previous-versions/dotnet/reactive-extensions/hh229114(v=vs.103)
     */
    public static class TestTwo
    {
        public static void Execute()
        {
            IObservable<Ticket> ticketObservable = Observable.Create(
                /*  Create accepts an argument "subscribe"
                 *      That is a function
                 *          That accepts an IObserver<T> and
                 *          returns an IDisposable
                 *  And returns and IObservable<T>
                 */
                (Func<IObserver<Ticket>, IDisposable>)TicketFactory.TicketSubscribe
                );

            //*********************************************************************************//
            //*** This is a sequence of tickets.
            //*** Display each ticket in the console window.
            //*********************************************************************************//
            using (IDisposable handle = ticketObservable.Subscribe(ticket => Console.WriteLine(ticket.ToString())))
            {
                Console.WriteLine("\nPress ENTER to unsubscribe...\n");
                Console.ReadLine();
            }
        }

        public class TicketFactory : IDisposable
        {
            bool bGenerate = true;
            internal TicketFactory(object ticketObserver)
            {
                //************************************************************************//
                //*** The sequence generator for tickets will be run on another thread
                //*** _Something_ needs to call back and generate events...
                //************************************************************************//
                Task.Factory.StartNew(
                    /* The Action to execute*/
                    new Action<object>(TicketGenerator),
                    /* Any argument(s) the Action needs*/
                    ticketObserver);

            }

            private void TicketGenerator(object observer)
            {
                IObserver<Ticket> ticketObserver = (IObserver<Ticket>)observer;

                //***********************************************************************************************//
                //*** Generate a new ticket every 3 sec and call the observer's
                //*** OnNext handler to deliver it. ***//
                //***********************************************************************************************//
                Ticket t;

                while (bGenerate)
                {
                    t = new Ticket(Guid.NewGuid().ToString());
                    ticketObserver.OnNext(t);
                    Thread.Sleep(3000);
                }
            }

            //********************************************************************************************************************************//
            //*** TicketSubscribe starts the flow of tickets for the ticket sequence
            //*** when a subscription is created. It is passed to       ***//
            //*** Observable.Create() as the subscribe parameter. Observable.Create()
            //*** returns the IObservable<Ticket> that is used to      ***//
            //*** create subscriptions by calling the IObservable<Ticket>.Subscribe() method.                                              ***//
            //***                                                                                                                          ***//
            //*** The IDisposable interface returned by TicketSubscribe is returned
            //*** from the IObservable<Ticket>.Subscribe() call. Calling ***//
            //*** Dispose cancels the subscription freeing ticket generating resources.                                                    ***//
            //********************************************************************************************************************************//
            public static IDisposable TicketSubscribe(object ticketObserver)
            {
                TicketFactory tf = new TicketFactory(ticketObserver);

                return tf;
            }

            public void Dispose()
            {
                throw new NotImplementedException();
            }
        }

        class Ticket
        {
            private readonly string ticketID;
            private readonly DateTime timeStamp;

            public Ticket(string tid)
            {
                ticketID = tid;
                timeStamp = DateTime.Now;
            }

            public override string ToString()
            {
                return $"Ticket ID : {ticketID}\nTimestamp : {timeStamp}\n";
            }
        }

    }
}
