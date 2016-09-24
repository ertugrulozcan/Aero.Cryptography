using Aero.Cryptography.Algorithms.Contracts;
using Aero.Cryptography.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aero.Cryptography.Algorithms.Rsa
{
    class PrivateKey : IPrivateKey, ISignatureKey
    {
        private BigInteger d;
        private BigInteger n;

        public BigInteger D
        {
            get
            {
                return d;
            }

            private set
            {
                d = value;
            }
        }

        public BigInteger N
        {
            get
            {
                return n;
            }

            private set
            {
                n = value;
            }
        }

        public BigInteger SignatureKey
        {
            get
            {
                return this.D;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="e"></param>
        /// <param name="n"></param>
        public PrivateKey(BigInteger e, BigInteger n)
        {
            this.D = e;
            this.N = n;
        }
    }
}
