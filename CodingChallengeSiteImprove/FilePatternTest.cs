using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingChallengeSiteImprove
{
    [TestClass]
    public class FilePatternTest
    {
        [TestMethod]
        public void TestResultsForASearchPattern1()
        {
            //Arrange
            string inputPattern = "Section \\d+ Refresh";
            string inputFile = @"C:\Users\sabal\Downloads\code challenge\opg1\Example input\1.html";
            string expectedResult = "9780103281";

            //Act
            FileSearch fileSearch = new FileSearch(inputFile, inputPattern);
            fileSearch.ScanFile();

            //Assert
            Assert.IsTrue(!string.IsNullOrEmpty(fileSearch.FormattedOutput));
            Assert.AreEqual(expectedResult, fileSearch.FormattedOutput.Trim(','));
        }

        [TestMethod]
        public void TestResultsForAllFilesWithSearchPattern1()
        {
            //Arrange
            string inputPattern = "Section \\d+ Refresh";
            string inputFolder = @"C:\Users\sabal\Downloads\code challenge\opg1\Example input";
            string expectedResult = string.Join(",", new List<string>()
            { "9780103281,9028094287" });

            //Act
            foreach (var file in Directory.GetFiles(inputFolder))
            {
                FileSearch fileSearch = new FileSearch(file, inputPattern);
                fileSearch.ScanUsingBufferStream();

                if (!string.IsNullOrEmpty(fileSearch.FormattedOutput))
                {
                    //Assert
                    Assert.IsNotNull(fileSearch.FormattedOutput);
                    Assert.IsTrue(expectedResult.Contains(fileSearch.FormattedOutput.Trim(',')));
                }
            }          
            
        }

        [TestMethod]
        public void TestResultsForASearchPattern3NoMatch ()
        {
            //Arrange
            string inputPattern = @"Siteimprove\s+Web\s+analytics";
            string inputFile = @"C:\Users\sabal\Downloads\code challenge\opg1\Example input\9.html";            

            //Act
            FileSearch fileSearch = new FileSearch(inputFile, inputPattern);
            fileSearch.ScanFile();

            //Assert            
            Assert.IsNull(fileSearch.FormattedOutput);
        }

        [TestMethod]
        public void TestResultsForASearchPattern3OneMatch()
        {
            //Arrange
            string inputPattern = @"Siteimprove\s+\S*Web\s+\S*analytics";
            string inputFile = @"C:\Users\sabal\Downloads\code challenge\opg1\Example input\9.html";
            string expectedResult = string.Join(",", new List<string>() { "9131522019" });

            //Act
            FileSearch fileSearch = new FileSearch(inputFile, inputPattern);
            fileSearch.ScanFile();

            //Assert
            Assert.IsNotNull(fileSearch.FormattedOutput);
            Assert.AreEqual(expectedResult, fileSearch.FormattedOutput.Trim(','));
        }
    }
}
