using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace SecretSanta.UserGiftImport.Tests
{
    [TestClass]
    public class UserGiftImporterTests
    {
        public UserGiftImporter Importer { get; set; }
        public string TestFilePath { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            Importer = new UserGiftImporter();
            TestFilePath = Environment.CurrentDirectory + "\\TestFile.txt";
        }

        [TestCleanup]
        public void TestCleanup()
        {

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Open_NullPath_ArgumentNullException()
        {
            Importer.Open(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Open_NonExistingFile_ArgumentException()
        {
            Importer.Open("nonExistingFile.txt");
        }

        [TestMethod]
        public void Open_ExistingFile_OpenStream() //FIXME: Test alternates in passing/failing because
                                                   // file owned by another process error.
        {
            //CreateTestFile();
            Importer.Open(TestFilePath);

            Assert.AreEqual(TestFilePath, Importer.StreamReader);

            Importer.Close();
            // DeleteTestFile();
        }

        [TestMethod]
        public void ReadNext_BlankLine_ReturnsEmptyString()
        {
            Importer.Open(TestFilePath);

            string line = Importer.ReadNext();

            Assert.AreEqual("", line);
        }
    }
}
