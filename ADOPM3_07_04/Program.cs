using System;
using System.Threading;

namespace ADOPM3_07_04
{
    class Program
    {
        static void Main(string[] args)
        {
            //Two int that MUST be exactly the same (but will not - NOT thread safe)
            int iUnsafeResult1= 0;  
            int iUnsafeResult2= 0;

            //Two int that MUST be exactly the same (and will be - Thread safe)
            object _locker = new object();
            int iSafeResult1 = 0;
            int iSafeResult2 = 0;

            new Thread(() =>
            {
                var rnd = new Random();
                for (int i = 0; i < 100; i++)
                {
                    iUnsafeResult1 = 1111;
                    Thread.Sleep(rnd.Next(1, 5));
                    iUnsafeResult2 = 1111;
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
            }).Start();

            new Thread(() =>
            {
                var rnd = new Random();
                for (int i = 0; i < 100; i++)
                {
                    iUnsafeResult1 = 2222;
                    Thread.Sleep(rnd.Next(1, 5));
                    iUnsafeResult2 = 2222;
    
                    if (iUnsafeResult1 != iUnsafeResult2)
                        Console.WriteLine("Unsafe mismatch");

                    
                    lock (_locker)
                    {
                        iSafeResult1 = 9999;
                        Thread.Sleep(rnd.Next(1, 5));
                        iSafeResult2 = 9999;

                         if (iSafeResult1 != iSafeResult2)
                            Console.WriteLine("Safe mismatch");
                    }
                    
                }
            }).Start();
        }
    }
}
