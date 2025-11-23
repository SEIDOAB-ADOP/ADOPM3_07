using System;
using System.Net.Http;
using System.Threading;

namespace ADOPM3_07_03
{
    public static class myExtensions
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
            //Data is not Thread safe, for illustration of variable capturing only
            int nrDownLoads = 0;        //Captured in LE - used as Thread input value and copied to local in the LE
            int nrCharDownloaded = 0;   //Captured in LE - used as Thread output parameter
            
            var t1 = new Thread((object url) =>
            {
                int localNrDownLoads = nrDownLoads;     //Copy the captured to a local variable
                string page = null;
                Console.WriteLine($"Downloading {url as string}");
                for (int i = 0; i < localNrDownLoads; i++)
                {
                    Console.WriteLine($"...t1:{i+1}");
                    page += httpClient.GetStringAsync(url as string).Result;
                }
                Console.WriteLine($"t1 Downloaded {url as string}, length {page.Length}");
                nrCharDownloaded += page.Length;
            });
            var t2 = new Thread((object url) =>
            {
                int localNrDownLoads = nrDownLoads;     //Copy the scoped to a local variable
                string page = null;
                Console.WriteLine($"Downloading {url as string}");
                for (int i = 0; i < localNrDownLoads; i++)
                {
                    Console.WriteLine($"...t2:{i+1}");
                    page += httpClient.GetStringAsync(url as string).Result;
                }
                Console.WriteLine($"t2 Downloaded {url as string}, length {page.Length}");
                nrCharDownloaded += page.Length;
            });

            nrDownLoads = 2;
            t1.Start("https://www.cnn.com/");

            nrDownLoads = 3;
            t2.Start("https://dotnet.microsoft.com/");

            t1.Join();
            t2.Join();
            Console.WriteLine($"Totalnr of character downloaded: {nrCharDownloaded}");
        }
    }
    //Exercise
    //1.    Identify and explain the difference scoped variables, captured variable and passing data as thread argument.
    //2.    Create your own LE task using scoped, captured variables as well as passing data as thread argument.
    //      Do you get data syncronization problems?
}
