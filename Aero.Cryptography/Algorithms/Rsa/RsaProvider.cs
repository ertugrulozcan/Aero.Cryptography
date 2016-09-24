using Aero.Cryptography.Algorithms.Contracts;
using Aero.Cryptography.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aero.Cryptography.Algorithms.Rsa
{
    // GEREKLİLİKLER:
    /*
     1) p ve q asallarının üretimi
     2) n = p*q
     3) Totient'in bulunması : T(n) = (p - 1)*(q - 1)
     4) 1 < e < T(n) aralığında ve EBOB(e, T(n))=1 olan bir e sayısı üretilmesi
     5) d.e % T(n) = 1 olacak biçimde bir d sayısı üretilmesi

     ORTAK ANAHTAR : (n, e)
     ÖZEL ANAHTAR : (n, d)

     NOT : d, p, q ve T(n) değerleri kesinlikle gizli kalmalıdır.
    */

    public class RsaProvider : IProvider
    {
        #region Constants

        private readonly int BITLENGTH;

        #endregion

        #region Fields

        private IEncryptor encoder;
        private IDecryptor decoder;
        private ISigner signer;

        #endregion

        #region Properties

        public IEncryptor Encoder
        {
            get
            {
                return this.encoder;
            }
            private set
            {
                this.encoder = value;
            }
        }

        public IDecryptor Decoder
        {
            get
            {
                return this.decoder;
            }
            private set
            {
                this.decoder = value;
            }
        }

        public ISigner Signer
        {
            get
            {
                return this.signer;
            }
            private set
            {
                this.signer = value;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        public RsaProvider(BitLength bitLength)
        {
            this.BITLENGTH = this.GetBitLength(bitLength);

            bool isValid = false;
            do
            {
                BigInteger p = this.GenerateBigPrime(this.BITLENGTH);
                BigInteger q = this.GenerateBigPrime(this.BITLENGTH);

                // (q, p'den farklı bir değerde olmalıdır)
                while (p == q)
                    q = this.GenerateBigPrime(this.BITLENGTH);

                BigInteger n = p * q;

                BigInteger totient = this.Totient(p, q);
                BigInteger e = this.GenerateE();
                BigInteger d = this.GenerateD(e, totient);

                var publicKey = new PublicKey(e, n);
                var privateKey = new PrivateKey(d, n);

                this.Encoder = new Encoder(publicKey);
                this.Decoder = new Decoder(privateKey);

                this.Signer = new RsaSigner(privateKey, publicKey);

                isValid = this.IsValid(totient, e);
            }
            while (!isValid);
        }

        #endregion

        #region Methods
        
        private BigInteger GenerateBigPrime(int bitLength)
        {
            //var rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            //byte[] bytes = new byte[bitLength / 8];
            //rng.GetBytes(bytes);

            //return new BigInteger(bytes);

            Random rand = new Random();
            var exBigInt = BigInteger.genPseudoPrime(bitLength, 5, rand);
            //return BigInteger.Parse(exBigInt.ToString());
            return exBigInt;
        }

        private BigInteger Totient(BigInteger x1, BigInteger x2)
        {
            return (x1 - 1) * (x2 - 1);
        }

        /// <summary>
        /// e ve t değeri için EBOB(e, T(n)) = 1 şartını kontrol eder.
        /// </summary>
        /// <returns></returns>
        private bool IsValid(BigInteger t, BigInteger e)
        {
            if (e < 1 || e > t)
                return false;

            else if (t % e == 0)
                return false;

            return true;
        }

        private BigInteger GenerateE()
        {
            return new BigInteger(65537);
        }
        
        private BigInteger GenerateD(BigInteger e, BigInteger totient)
        {
            BigInteger d = e.modInverse(totient);

            if (d < 0)
                d += totient;

            return d;
        }

        private int GetBitLength(BitLength bL)
        {
            switch (bL)
            {
                case BitLength.RSA128: return 128;
                case BitLength.RSA256: return 256;
                case BitLength.RSA512: return 512;
                case BitLength.RSA1024: return 1024;
                //case BitLength.RSA2048: return 2048;
                //case BitLength.RSA4096: return 4096;
                default: return 2048;
            }
        }

        #endregion

        #region Enumerators

        public enum BitLength
        {
            RSA128,
            RSA256,
            RSA512,
            RSA1024,
            //RSA2048,
            //RSA4096,
        }

        #endregion
    }
}
