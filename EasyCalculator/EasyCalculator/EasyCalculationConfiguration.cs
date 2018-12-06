using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCalculator
{
    public class EasyCalculationConfiguration
    {
        public string headerPattern { get; }
        public string footerPattern { get; }
        public string CRLF  { get; }
        public string CR { get; }
        public string LF { get; }

        public EasyCalculationConfiguration()
        {
            headerPattern = "<!--:Begin:Chksum:{{AlgorithmId}}:-->";
            footerPattern = "<!--:End:Chksum:{{AlgorithmId}}:{{c1}}:{{c2}}:-->";
            CRLF = "\r\n";
            CR = "\r";
            LF = "\n";
        }

        public EasyCalculationConfiguration(string headerPattern, string footerPattern, string crlf, string cr, string lf)
        {
            this.headerPattern = headerPattern;
            this.footerPattern = footerPattern;
            this.CRLF = crlf;
            this.CR = cr;
            this.LF = lf;
        }
    }
}
