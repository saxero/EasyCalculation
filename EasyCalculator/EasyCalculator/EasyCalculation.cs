using System;
using System.Runtime.Remoting.Messaging;

namespace EasyCalculator
{
    public class EasyCalculation: IChecksumCalculator
    {
        private const string headerPattern = "<!--:Begin:Chksum:{{AlgorithmId}}:-->";
        private const string footerPattern = "<!--:End:Chksum:{{AlgorithmId}}:{{c1}}:{{c2}}:-->";

        public bool VerifyChecksum(string message)
        {
            throw new NotImplementedException();
        }

        public string CalculateChecksum(string message)
        {
            var calcChecksum = this.CalculateEasyChecksum(message);
            var transferredChecksum = this.CalculateTransferredChecksum(calcChecksum);

            var header = headerPattern.Replace("{{AlgorithmId}}", "1");
            var footer = footerPattern.Replace("{{AlgorithmId}}", "1")
                .Replace("{{c1}}", transferredChecksum.Checksum1.ToString())
                .Replace("{{c2}}", transferredChecksum.Checksum2.ToString());

            return header + "\r\n" + message + "\r\n" + footer;
        }

        private EasyCalculationChecksumPair CalculateEasyChecksum(string message)
        {
            byte cs1 = 0;
            byte cs2 = 0;

            // Always use "\n" as line break when calculating the checksum.
            message = message.Replace("\r\n", "\n"); // Find and replace CR LF with LF
            message = message.Replace("\r", "\n"); // Find and replace CR with LF.
            for (var i = 0; i < message.Length; i++)
            {
                cs1 += (byte)message[i];
                cs2 += cs1;
            }

            return new EasyCalculationChecksumPair()
            {
                Checksum1 = cs1,
                Checksum2 = cs2
            };
        }

        private EasyCalculationChecksumPair CalculateTransferredChecksum(EasyCalculationChecksumPair calculatedChecksum)
        {
            byte t1 = (byte) (-calculatedChecksum.Checksum1 - calculatedChecksum.Checksum2);
            byte t2 = (byte)(-calculatedChecksum.Checksum1 - t1);

            return new EasyCalculationChecksumPair()
            {
                Checksum1 = t1,
                Checksum2 = t2
            };
        }
    }
}
