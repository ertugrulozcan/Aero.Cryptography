using Aero.Cryptography.Algorithms.Contracts;
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

        /// <summary>
        /// Constructor
        /// </summary>
        public Cypher(BigInteger value)
        {
            this.TrigintaHexValue = new TrigintaHex(value);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public Cypher(string str)
        {
            this.TrigintaHexValue = TrigintaHex.Parse(str);
        }

        public override string ToString()
        {
            return this.TrigintaHexValue.ToString(4);
        }
    }
}
