using System;
using System.Net;
using System.Threading;

namespace ADOPM3_07_06
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting Main");
            using var signal = new AutoResetEvent(false);
            //using var signal = new ManualResetEvent(false);

            Console.WriteLine("Starting Thread1");
            var t1 = new Thread(() =>
            {
                Console.WriteLine("Thread1 waiting for signal...");
                Thread.Sleep(new Random().Next(1, 100));
                signal.WaitOne();
                Console.WriteLine("Thread1 got signal!");
            });
            t1.Start();

            Console.WriteLine("Starting Thread2");
            var t2 = new Thread(() =>
            {
                Console.WriteLine("Thread2 waiting for signal...");
                Thread.Sleep(new Random().Next(1, 100));
                signal.WaitOne();
                Console.WriteLine("Thread2 got signal!");
            });
            t2.Start();

            var t3 = new Thread(() =>
            {
                Console.WriteLine("Thread3 waiting for signal...");
                Thread.Sleep(new Random().Next(1, 100));
                signal.WaitOne();
                Console.WriteLine("Thread3 got signal!");
            });
            t3.Start();

            Console.WriteLine("Main waits for user input");
            Console.ReadKey();

            Console.WriteLine("\nMain sends signal to thread");
            signal.Set();        // “Open” the signal

            Console.WriteLine("Main waits for user input");
            Console.ReadKey();

            Console.WriteLine("\nMain sends signal to thread");
            signal.Set();        // “Open” the signal

            Console.WriteLine("Main waits for user input");
            Console.ReadKey();

            Console.WriteLine("\nMain sends signal to thread");
            signal.Set();        // “Open” the signal

            t1.Join();
            t2.Join();
            t3.Join();
        }
    }
    //Exercise
    //1.    Create another thread waiting for the same signal. What happens?
}
