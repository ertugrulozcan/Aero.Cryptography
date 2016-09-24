using Aero.Cryptography.Contracts;
using Aero.Cryptography.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aero.Cryptography.Algorithms.Rsa
{
    public class RsaSigner : ISigner
    {
        public ISignatureKey SignatureKey
        {
            get;
        }

        public ISignatureKey VerificationKey
        {
            get;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="signKey"></param>
        public RsaSigner(ISignatureKey signKey, ISignatureKey verifyKey)
        {
            this.SignatureKey = signKey;
            this.VerificationKey = verifyKey;
        }

        public byte[] Hash(byte[] data)
        {
            SHA3.SHA3Managed sha3 = new SHA3.SHA3Managed(256);
            return sha3.ComputeHash(data).Take(32).ToArray();
        }

        public byte[] Sign(byte[] data)
        {
            return this.Sign(data, this.SignatureKey);
        }

        public byte[] Sign(byte[] data, ISignatureKey signatureKey)
        {
            if (signatureKey is PrivateKey)
            {
                var privateKey = signatureKey as PrivateKey;

                var hash = this.Hash(data);

                if (new BigInteger(hash) > privateKey.N)
                    throw new Exception("Hash function is not suitable for signature");

                BigInteger c = new BigInteger(hash);
                var sign = c.modPow(privateKey.D, privateKey.N);
                return sign.getBytes();
            }
            else
            {
                throw new Exception("SignatureKey parameter format was not RsaPrivateKey!");
            }
        }

        public bool Verify(byte[] data, byte[] sign)
        {
            return this.Verify(data, sign, this.VerificationKey);
        }

        public bool Verify(byte[] data, byte[] sign, ISignatureKey verificationKey)
        {
            if (verificationKey is PublicKey)
            {
                var publicKey = verificationKey as PublicKey;

                var hash = this.Hash(data);

                if (new BigInteger(hash) > publicKey.N)
                    throw new Exception("Hash function is not suitable for signature validation.");

                BigInteger m = new BigInteger(sign);
                var c = m.modPow(publicKey.E, publicKey.N);
                var decryptedHash = c.getBytes();

                return ByteArrayCompare(decryptedHash, hash);
            }
            else
            {
                throw new Exception("VerificationKey parameter format was not RsaPublicKey!");
            }
        }

        /// <summary>
        /// Compare byte arrays
        /// Copyright (c) 2008-2013 Hafthor Stefansson
        /// Distributed under the MIT/X11 software license
        /// Ref: http://www.opensource.org/licenses/mit-license.php
        /// </summary>
        /// <param name="array1"></param>
        /// <param name="array2"></param>
        /// <returns></returns>
        internal static unsafe bool ByteArrayCompare(byte[] array1, byte[] array2)
        {
            if (array1 == null || array2 == null || array1.Length != array2.Length)
                return false;

            fixed (byte* p1 = array1, p2 = array2)
            {
                byte* x1 = p1, x2 = p2;
                int l = array1.Length;

                for (int i = 0; i < l / 8; i++, x1 += 8, x2 += 8)
                {
                    if (*((long*)x1) != *((long*)x2))
                        return false;
                }

                if ((l & 4) != 0)
                {
                    if (*((int*)x1) != *((int*)x2))
                        return false;

                    x1 += 4; x2 += 4;
                }

                if ((l & 2) != 0)
                {
                    if (*((short*)x1) != *((short*)x2))
                        return false;

                    x1 += 2; x2 += 2;
                }

                if ((l & 1) != 0)
                {
                    if (*((byte*)x1) != *((byte*)x2))
                        return false;
                }

                return true;
            }
        }
    }
}
