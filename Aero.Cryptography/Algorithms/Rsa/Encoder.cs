﻿using Aero.Cryptography.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aero.Cryptography.Utilities;

namespace Aero.Cryptography.Algorithms.Rsa
{
    public class Encoder : IEncryptor
    {
        public readonly string CryptoAlgorithm;

        private IPatternConverter patternConverter;

        private PublicKey RsaPublicKey { get; set; }

        public IPatternConverter PatternConverter
        {
            get
            {
                return patternConverter;
            }

            private set
            {
                patternConverter = value;
            }
        }

        /// <summary>
        /// Constructor 1
        /// </summary>
        /// <param name="publicKey"></param>
        internal Encoder(PublicKey publicKey, string CryptoAlgorithm = "")
        {
            this.RsaPublicKey = publicKey;

            this.CryptoAlgorithm = CryptoAlgorithm;
        }

        /// <summary>
        /// Constructor 2
        /// </summary>
        /// <param name="publicKey"></param>
        /// <param name="patternConverter"></param>
        internal Encoder(PublicKey publicKey, IPatternConverter patternConverter, string CryptoAlgorithm = "")
        {
            this.RsaPublicKey = publicKey;
            this.PatternConverter = patternConverter;

            this.CryptoAlgorithm = CryptoAlgorithm;
        }

        public ISecret Encrypt(byte[] obj)
        {
            if (this.RsaPublicKey is PublicKey)
            {
                BigInteger m;
                m = this.PatternToNumber(obj);

                if (m > this.RsaPublicKey.N)
                    throw new Exception("Message is too long!");

                return new Cypher(m.modPow(this.RsaPublicKey.E, this.RsaPublicKey.N), null, this.CryptoAlgorithm);
            }
            else
                throw new Exception("PublicKey format was not RsaPublicKey");
        }

        public ISecret Encrypt(byte[] obj, IPublicKey publicKey)
        {
            if (publicKey is PublicKey)
            {
                BigInteger m;
                m = this.PatternToNumber(obj);

                if (m > this.RsaPublicKey.N)
                    throw new Exception("Message is too long!");

                return new Cypher(m.modPow((publicKey as PublicKey).E, (publicKey as PublicKey).N), null, this.CryptoAlgorithm);
            }
            else
                throw new Exception("PublicKey format was not RsaPublicKey");
        }

        public BigInteger PatternToNumber(byte[] obj)
        {
            if (this.PatternConverter == null)
                return BasicPatternConverter.Current.ConvertToBigInteger(obj);
            else
                return this.PatternConverter.ConvertToBigInteger(obj);
        }
    }
}
