using System;
using System.IO;
using System.Threading;

namespace CodingChallengeSiteImprove
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Enter FolderPath which has input files " +
                            "and SearchPattern in double quotes separated by space.");
                Console.Read();
            }
            else
            {
                //Added default arguments in
                //project debug - command line section
                string folderPath = args[0];
                var inputPattern = args[1];

                Console.WriteLine("Pattern: " + inputPattern);
                foreach (var file in Directory.GetFiles(folderPath))
                {
                    Thread thread = new Thread(() => new FileSearch(file, inputPattern).ScanFile());
                    thread.Start();
                }
            }
        }
    }
}
