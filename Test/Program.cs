using System;
using System.Threading.Tasks;
using Test.Args;

namespace Test
{


    class Program
    {
        static async Task Main(string[] args)
        {
            EventManager eventManager = new EventManager();

            eventManager.Of<EventArgs1>().Subscriptions += Program_Subscriptions_EventArgs1;
            eventManager.Of<EventArgs2>().Subscriptions += Program_Subscriptions_EventArgs2;
            eventManager.Of<EventArgs3>().Subscriptions += Program_Subscriptions_EventArgs3;

            eventManager.RaiseEvent(new EventArgs1());
            eventManager.RaiseEvent(new EventArgs2());
            eventManager.RaiseEvent(new EventArgs3());

            //Exemple pour  attendre un évènement en particulier
            TaskCompletionSource<QuitEventArgs> taskCompletion = new TaskCompletionSource<QuitEventArgs>();

            EventManager eventManager2 = new EventManager();

            eventManager2.Of<QuitEventArgs>().Subscriptions += (sender, eventArgs) =>
            {
                taskCompletion.TrySetResult(eventArgs);
            };

            //async task - for raise an event
            Start(eventManager2);

            var result = await taskCompletion.Task;

            //continue your code here
            Console.WriteLine("Close");
        }

        private static void Start(EventManager eventManager2)
        {
            Task.Run(async () =>
            {
                await Task.Delay(1000);
                eventManager2.RaiseEvent(new QuitEventArgs());
            });
        }

        private static void Program_Subscriptions_EventArgs1(object sender, EventArgs1 e)
        {
            Console.WriteLine("Receive an EventArgs1");
        }
        private static void Program_Subscriptions_EventArgs2(object sender, EventArgs2 e)
        {
            Console.WriteLine("Receive an EventArgs2");
        }
        private static void Program_Subscriptions_EventArgs3(object sender, EventArgs3 e)
        {
            Console.WriteLine("Receive an EventArgs3");
        }
    }
}
