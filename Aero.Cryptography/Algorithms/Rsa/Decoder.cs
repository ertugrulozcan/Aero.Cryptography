using Aero.Cryptography.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aero.Cryptography.Utilities;

namespace Aero.Cryptography.Algorithms.Rsa
{
    class Decoder : IDecryptor
    {
        private IPatternConverter patternConverter;

        private PrivateKey RsaPrivateKey { get; set; }

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
        /// <param name="privateKey"></param>
        internal Decoder(PrivateKey privateKey)
        {
            this.RsaPrivateKey = privateKey;
        }

        /// <summary>
        /// Constructor 2
        /// </summary>
        /// <param name="privateKey"></param>
        /// <param name="patternConverter"></param>
        internal Decoder(PrivateKey privateKey, IPatternConverter patternConverter)
        {
            this.RsaPrivateKey = privateKey;
            this.PatternConverter = patternConverter;
        }

        public byte[] Decrypt(ISecret crypto)
        {
            if (crypto is Cypher && this.RsaPrivateKey is PrivateKey)
            {
                BigInteger c = (crypto as Cypher).Value;
                var m = c.modPow(this.RsaPrivateKey.D, this.RsaPrivateKey.N);

                if (this.PatternConverter == null)
                    return BasicPatternConverter.Current.ConvertToByteArray(m);
                else
                    return this.PatternConverter.ConvertToByteArray(m);
            }
            else
                throw new Exception("Secret format is incompatible with algorithm or PrivateKey format was not RsaPrivateKey.");
        }

        public byte[] Decrypt(ISecret crypto, IPrivateKey privateKey)
        {
            if (crypto is Cypher && privateKey is PrivateKey)
            {
                BigInteger c = (crypto as Cypher).Value;
                var m = c.modPow((privateKey as PrivateKey).D, (privateKey as PrivateKey).N);
                
                if (this.PatternConverter == null)
                    return BasicPatternConverter.Current.ConvertToByteArray(m);
                else
                    return this.PatternConverter.ConvertToByteArray(m);
            }
            else
                throw new Exception("Secret format is incompatible with algorithm or PrivateKey format was not RsaPrivateKey.");
        }
    }
}
