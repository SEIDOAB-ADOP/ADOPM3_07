using System;
using System.Net.Http;
using System.Threading;

namespace ADOPM3_07_02
{
    //Again, Extension metods are practical
    public static class ThreadExtensions
    {
        public static ThreadState UsefulStates(this ThreadState ts)
        {
            return ts & (ThreadState.Unstarted | ThreadState.Running | ThreadState.WaitSleepJoin | ThreadState.Stopped);
        }
    }
    class Program
    {
        private static readonly HttpClient httpClient = new HttpClient();

        private static void Main(string[] args)
        {
            var t1 = new Thread(MyThreadEntryPoint);
            var t2 = new Thread(MyThreadEntryPoint);
            var t3 = new Thread(MyThreadEntryPoint);

            t1.Start("https://www.cnn.com/");
            Console.WriteLine($"t1 {t1.ThreadState.UsefulStates()}");
            t1.Join();
            Console.WriteLine($"t1 {t1.ThreadState.UsefulStates()}");

            t2.Start("https://www.bbc.com/");
            t2.Join();
            t3.Start("https://dotnet.microsoft.com/");
            t3.Join();
            Console.WriteLine($"t3 {t1.ThreadState.UsefulStates()}");

            
            Console.WriteLine("Sleep 5 seconds");
            Thread.Sleep(5000);
            Console.WriteLine("Done Sleeping!");
            
            Console.WriteLine("Main finished");
        }

        private static void MyThreadEntryPoint(object arg)
        {
            string url = (string)arg;

            Console.WriteLine($"Downloading {url}");
            try
            {
                string page = httpClient.GetStringAsync(url).Result;
                Console.WriteLine($"Downloaded {url}, length {page.Length}");
            }
            catch
            {
                Console.WriteLine("Connection error");
            }
        }

     }
    //Exercise
    //1.    Experiment by moving or removing some of the Join() and Sleep(). Discuss in the group what happens?
}
