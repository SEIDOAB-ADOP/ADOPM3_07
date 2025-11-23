namespace ADOPM3_07_04a
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Two int that MUST be exactly the same (but will not be - NOT thread safe)
            int iUnsafeResult1 = 0;   //for example a bank account
            int iUnsafeResult2 = 0;   //for example a transaction amount

            //Two int that MUST be exactly the same (and will be - Thread safe)
            object _locker = new object();
            int iSafeResult1 = 0;
            int iSafeResult2 = 0;

            var t1 = Task.Run(() =>
            {
                var rnd = new Random();
                for (int i = 0; i < 100; i++)
                {
                    iUnsafeResult1 = 1111;
                    Thread.Sleep(rnd.Next(1, 5));   //This delay to simulate slower computer - causes error
                    iUnsafeResult2 = 1111;

                    //They must be equal - or are they???
                    if (iUnsafeResult1 != iUnsafeResult2)
                        Console.WriteLine("Unsafe mismatch");

                    
                    lock (_locker)
                    {
                        iSafeResult1 = 8888;
                        Thread.Sleep(rnd.Next(1, 5));
                        iSafeResult2 = 8888;

                        if (iSafeResult1 != iSafeResult2)
                            Console.WriteLine("Safe mismatch");
                    }
                    
                }
                Console.WriteLine("t1 Finished");
            });

            var t2 = Task.Run(() =>
            {
                var rnd = new Random();
                for (int i = 0; i < 100; i++)
                {
                    iUnsafeResult1 = 2222;
                    Thread.Sleep(rnd.Next(1, 5));   //This delay to simulate slower computer - causes error
                    iUnsafeResult2 = 2222;

                    //They must be equal - or are they???
                    if (iUnsafeResult1 != iUnsafeResult2)
                        Console.WriteLine("Unsafe mismatch");

                    
//                    lock (_locker)
                    {
                        iSafeResult1 = 9999;
                        Thread.Sleep(rnd.Next(1, 5));
                        iSafeResult2 = 9999;

                        if (iSafeResult1 != iSafeResult2)
                            Console.WriteLine("Safe mismatch");
                    }
                    
                }
                Console.WriteLine("t2 Finished");
            });

            Task.WaitAll(t1, t2);
            Console.WriteLine("All Finished");
        }
    }
}