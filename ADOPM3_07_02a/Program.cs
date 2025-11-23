namespace ADOPM3_07_02a
{
    internal class Program
    {
        private static readonly HttpClient httpClient = new HttpClient();

        static void Main(string[] args)
        {
            var t1 = Task.Run(() => MyThreadEntryPointAsync("https://www.cnn.com/"));
            Console.WriteLine($"t1 status: {t1.Status}");
            //t1.Wait();

            var t2 = Task.Run(() => MyThreadEntryPointAsync("https://www.bbc.com/"));
            Console.WriteLine($"t1 status: {t1.Status}");
            Console.WriteLine($"t2 status: {t2.Status}");
            //t2.Wait();

            var t3 = Task.Run(() => MyThreadEntryPointAsync("https://dotnet.microsoft.com/"));
            Console.WriteLine($"t1 status: {t1.Status}");
            Console.WriteLine($"t2 status: {t2.Status}");
            Console.WriteLine($"t3 status: {t3.Status}");
            //t3.Wait();

            //Location of Join matters...
            Task.WaitAll(t1, t2, t3);
            Console.WriteLine("All Tasks finished");

            Console.WriteLine("Main is finished");
            Console.WriteLine($"t1 status: {t1.Status}");
            Console.WriteLine($"t2 status: {t2.Status}");
            Console.WriteLine($"t3 status: {t3.Status}");
        }
        private static async Task MyThreadEntryPointAsync(object arg)
        {
            string url = (string)arg;
            Console.WriteLine($"Downloading {url}");
            try
            {
                string page = await httpClient.GetStringAsync(url);
                Console.WriteLine($"Downloaded {url}, length {page.Length}");
            }
            catch
            {
                Console.WriteLine("Connection error");
            }
        }
    }
}