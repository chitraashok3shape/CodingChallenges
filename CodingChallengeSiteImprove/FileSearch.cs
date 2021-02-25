using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CodingChallengeSiteImprove
{
    internal class FileSearch
    {
        public string FormattedOutput { get; set; }
        public string InputFilePath { get; }
        public string InputPattern { get; }
        public FileSearch(string inputFilePath, string inputPattern)
        {
            InputFilePath = inputFilePath;
            InputPattern = inputPattern;
        }

        /// <summary>
        /// Scans the file by traditional approach
        /// </summary>
        public void ScanFile()
        {
            try
            {
                string newPattern = @"\d+:.*("+InputPattern+")"; //should include\n
                var regex = new Regex(newPattern, RegexOptions.Multiline);

                foreach (var line in File.ReadLines(InputFilePath, Encoding.UTF8)
                    .Where(line => regex.Match(line).Success))
                {
                    FormattedOutput += string.Join(",", line.Substring(0, line.IndexOf(":")));
                }

                PrintResults();

            }
            catch (DirectoryNotFoundException exception)
            {
                throw new DirectoryNotFoundException(exception.Message);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// Scans the file using BufferStream
        /// </summary>
        public void ScanUsingBufferStream()
        {
            try
            {
                using FileStream fileStream = File.Open(InputFilePath
                            , FileMode.Open, FileAccess.Read, FileShare.Read);
                using BufferedStream bufferStream = new BufferedStream(fileStream);
                using StreamReader streamReader = new StreamReader(bufferStream, Encoding.UTF8);

                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    var regex = new Regex(InputPattern, RegexOptions.Multiline);

                    if (regex.Match(line).Success)
                    {
                        FormattedOutput += string.Join(",", line.Substring(0, line.IndexOf(":")));
                        PrintResults();
                    }
                }
            }
            catch (DirectoryNotFoundException exception)
            {
                throw new DirectoryNotFoundException(exception.Message);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }


        /// <summary>
        /// Prints the output
        /// </summary>
        private void PrintResults()
        {
            Console.WriteLine("Results: " + FormattedOutput);
        }
    }
}