using System;

namespace EasyCalculator
{
    public class EasyCalculation: IChecksumCalculator
    {
        private readonly string headerPattern;
        private readonly string footerPattern;
        private readonly string CRLF;
        private readonly string CR;
        private readonly string LF;

        public EasyCalculation() : this(new EasyCalculationConfiguration())
        {

        }
        
        public EasyCalculation(EasyCalculationConfiguration configuration)
        {
            headerPattern = configuration.headerPattern;
            footerPattern = configuration.footerPattern;
            CRLF = configuration.CRLF;
            CR = configuration.CR;
            LF = configuration.LF;
        }
        
        public bool VerifyChecksum(string message)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds the checksum header and footer for a given message.
        /// </summary>
        /// <param name="message">The original message for which the checksum is to be calculated.</param>
        /// <returns>The original message plus the header and footer with the corresponding transferred checksum values.</returns>
        public string GetMessageWithChecksumHeaderAndFooter(string message)
        {
            var calcChecksum = this.CalculateEasyChecksum(message);
            var transferredChecksum = this.CalculateTransferredChecksum(calcChecksum);

            var header = headerPattern.Replace("{{AlgorithmId}}", "1");
            var footer = footerPattern.Replace("{{AlgorithmId}}", "1")
                .Replace("{{c1}}", transferredChecksum.Item1.ToString())
                .Replace("{{c2}}", transferredChecksum.Item2.ToString());

            return header + CRLF + message + CRLF + footer;
        }

        /// <summary>
        /// Applies the EasyCalculation algorithm to a message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>A tuple of two bytes with the calculated checksum values.</returns>
        private Tuple<byte, byte> CalculateEasyChecksum(string message)
        {
            byte cs1 = 0;
            byte cs2 = 0;

            // Always use "\n" (LF) as line break when calculating the checksum.
            message = message.Replace(CRLF, LF); // Find and replace CR LF with LF
            message = message.Replace(CR, LF); // Find and replace CR with LF.
            for (var i = 0; i < message.Length; i++)
            {
                cs1 += (byte)message[i];
                cs2 += cs1;
            }

            return new Tuple<byte, byte>(cs1, cs2);
        }

        /// <summary>
        /// Calculates the transferred checksum values corresponding to calculated checksum values.
        /// </summary>
        /// <param name="calculatedChecksum">A tuple of two bytes corresponding to the calculated checksum.</param>
        /// <returns>A tuple of two bytes corresponding to the transferred checksum.</returns>
        private Tuple<byte,byte> CalculateTransferredChecksum(Tuple<byte,byte> calculatedChecksum)
        {
            byte t1 = (byte) (-calculatedChecksum.Item1 - calculatedChecksum.Item2);
            byte t2 = (byte) (-calculatedChecksum.Item1 - t1);

            return new Tuple<byte, byte>(t1, t2);
        }
    }
}
