namespace ADOPM3_07_05a
{
    internal class Program
    {
        public class SafeData
        {
            object _locker = new object();
            string SafeFile;
            int iSafeResult1 = 0;
            int iSafeResult2 = 0;
            Random rnd = new Random();


            public void SetData(string fname, int sf1, int sf2)
            {
                lock (_locker)
                {
                    SafeFile = fname;
                    iSafeResult1 = sf1;
                    Thread.Sleep(rnd.Next(1, 5));
                    iSafeResult2 = sf2;

                    if (iSafeResult1 != iSafeResult2 || SafeFile != $"file{iSafeResult1}.txt")
                        Console.WriteLine($"mySafe mismatch: iSafeResult1:{iSafeResult1}, iSafeResult2:{iSafeResult2}, SafeFile:{SafeFile}");
                }
            }
            public (string, int, int) GetData()
            {
                lock (_locker) 
                { 
                    return (SafeFile, iSafeResult1, iSafeResult2); 
                }
            }
        }

        static void Main(string[] args)
        {
            var SafeStorage = new SafeData();

            var t1 = Task.Run(() =>
            {
                var rnd = new Random();
                for (int i = 0; i < 1_000; i++)
                {
                    SafeStorage.SetData("file8888.txt", 8888, 8888);
                    (string fname, int i1, int i2) = SafeStorage.GetData();
                    if (i1 != i2 || fname != $"file{i1}.txt")
                        Console.WriteLine($"mySafe mismatch: i1:{i1}, i2:{i2}, fname:{fname}");
                }
                Console.WriteLine("t1 Finished");
            });

            var t2 = Task.Run(() =>
            {
                var rnd = new Random();
                for (int i = 0; i < 1_000; i++)
                {
                    SafeStorage.SetData("file1111.txt", 1111, 1111);
                    (string fname, int i1, int i2) = SafeStorage.GetData();
                    if (i1 != i2 || fname != $"file{i1}.txt")
                        Console.WriteLine($"mySafe mismatch: i1:{i1}, i2:{i2}, fname:{fname}");
                }
                Console.WriteLine("t2 Finished");
            });

            Task.WaitAll(t1, t2);
            Console.WriteLine("All Finished");
        }
    }    
    
    //Exercise
    //1.    Discuss withing the group, how would you manage addition or substratcion of iSafeResult1 and iSafeResult2? 
    //      Try it by starting a couple of Taks making additions and subtraction. How do you manage data integrity?
    //2.    Create your own safe Data class for a Generic type T. Test it by starting a couple of Tasks accessing and modyfying the data
    //3.    Discuss in the group. What is thread safe? is your safe Data class thread safe? why?
}