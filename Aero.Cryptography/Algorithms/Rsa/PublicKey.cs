﻿using Aero.Cryptography.Contracts;
using Aero.Cryptography.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aero.Cryptography.Algorithms.Rsa
{
    public class PublicKey : IPublicKey, ISignatureKey
    {
        private BigInteger e;
        private BigInteger n;

        public BigInteger E
        {
            get
            {
                return e;
            }

            private set
            {
                e = value;
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
                return this.E;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="e"></param>
        /// <param name="n"></param>
        public PublicKey(BigInteger e, BigInteger n)
        {
            this.E = e;
            this.N = n;
        }
    }
}
