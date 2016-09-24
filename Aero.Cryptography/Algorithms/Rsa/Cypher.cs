using Aero.Cryptography.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aero.Cryptography.Utilities;

namespace Aero.Cryptography.Algorithms.Rsa
{
    public class Cypher : ISecret
    {
        private TrigintaHex trigintaHexValue;
        
        public TrigintaHex TrigintaHexValue
        {
            get
            {
                return this.trigintaHexValue;
            }
            private set
            {
                this.trigintaHexValue = value;
                this.Value = this.trigintaHexValue.Value;
            }
        }

        public BigInteger Value { get; private set; }

        public byte[] Sign { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Cypher(BigInteger value, byte[] sign = null)
        {
            this.TrigintaHexValue = new TrigintaHex(value);
            this.Sign = sign;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public Cypher(string str, byte[] sign = null)
        {
            this.TrigintaHexValue = TrigintaHex.Parse(str);
            this.Sign = sign;
        }

        public override string ToString()
        {
            string cert = string.Empty;

            string cypher = this.TrigintaHexValue.ToString(4);

            cert += "<secret>" + Environment.NewLine;
            cert += "\t";
            cert += "<cypher length=\"" + cypher.Length + "\">" + Environment.NewLine;
            cert += "\t";
            cert += "\t";
            cert += cypher + Environment.NewLine;
            cert += "\t";
            cert += "</cypher>" + Environment.NewLine;

            string sign = (this.Sign != null ? BasicPatternConverter.Current.ConvertToBigInteger(this.Sign).ToString(16) : string.Empty);

            cert += "\t";
            cert += "<sign length=\"" + sign.Length + "\">" + Environment.NewLine;
            cert += "\t";
            cert += "\t";
            cert += sign + Environment.NewLine;
            cert += "\t";
            cert += "</sign>" + Environment.NewLine;

            cert += "</secret>";

            return cert;
        }
    }
}
