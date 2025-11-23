namespace ADOPM3_07_06a
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting Main");
            //using var signal = new AutoResetEvent(false);
            using var signal = new ManualResetEvent(false);

            Console.WriteLine("Starting t1");
            var t1 = Task.Run(() =>
            {
                Console.WriteLine("t1 waiting for signal...");
                Thread.Sleep(new Random().Next(1, 100));
                signal.WaitOne();
                Console.WriteLine("t1 got signal!");
            });

            Console.WriteLine("Starting t2");
            var t2 = Task.Run(() =>
            {
                Console.WriteLine("t2 waiting for signal...");
                Thread.Sleep(new Random().Next(1, 100));
                signal.WaitOne();
                Console.WriteLine("t2 got signal!");
            });

            var t3 = Task.Run(() =>
            {
                Console.WriteLine("t3 waiting for signal...");
                Thread.Sleep(new Random().Next(1, 100));
                signal.WaitOne();
                Console.WriteLine("t3 got signal!");
            });

            Console.WriteLine("Main waits for user input");
            Console.Read();

            Console.WriteLine("\nMain sends signal to task");
            signal.Set();        // “Open” the signal

            Console.WriteLine("Main waits for user input");
            Console.Read();

            Console.WriteLine("\nMain sends signal to task");
            signal.Set();        // “Open” the signal

            Console.WriteLine("Main waits for user input");
            Console.Read();

            Console.WriteLine("\nMain sends signal to task");
            signal.Set();        // “Open” the signal

            Task.WaitAll(t1,t2,t3);
            Console.WriteLine("All Finished");
        }
    }
    //Exercise
    //1.    Create another Task waiting for the same signal. What happens?
}