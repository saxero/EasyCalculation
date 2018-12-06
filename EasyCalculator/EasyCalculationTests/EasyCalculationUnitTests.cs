using System.Text.RegularExpressions;
using EasyCalculator;
using NUnit.Framework;

namespace EasyCalculationTests
{
    [TestFixture]
    public class EasyCalculationUnitTests: TestBase
    {
        private const string checksumPattern = "^<!--:Begin:Chksum:1:-->([\\s\\S]*)<!--:End:Chksum:1:[\\d]+:[\\d]+:-->$";
        
        [Test]
        public void CalculateChecksum_ValidXmlMessage_ChecksumIsCalculated()
        {
            // Arrange
            string text = this.ReadResourceFile(@"TestFiles.SampleXml.txt");
            
            // Act
            var easyCalc = new EasyCalculation();
            var result = easyCalc.GetMessageWithChecksumHeaderAndFooter(text);
            
            // Assert
            Assert.IsTrue(Regex.IsMatch(result, checksumPattern));
        }

        [Test]
        [TestCase(@"TestFiles.AckMsg.xml", @"TestFiles.AckMsgWithChecksum.xml")]
        [TestCase(@"TestFiles.SampleXml.txt", @"TestFiles.SampleXmlWithChecksum.txt")]
        public void CalculateChecksum_ValidAckMessage_ChecksumIsCalculated(string fileName, string fileNameWithChecksum)
        {
            // Arrange
            var text = this.ReadResourceFile(fileName);
            var expected = this.ReadResourceFile(fileNameWithChecksum);

            // Act
            var easyCalc = new EasyCalculation();
            var result = easyCalc.GetMessageWithChecksumHeaderAndFooter(text);

            // Assert
            Assert.AreEqual(result, expected);
        }
    }
}
