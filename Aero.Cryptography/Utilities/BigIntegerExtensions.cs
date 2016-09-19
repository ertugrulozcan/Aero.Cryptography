using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aero.Cryptography.Utilities
{
    public static class BigIntegerExtensions
    {
        public static BigInteger Pow(this BigInteger me, int exp)
        {
            if (exp < 0)
                throw new Exception("Exponent must be positive!");

            if (exp == 0)
                return 1;

            BigInteger pow = new BigInteger(me);

            for(int i = 0; i < exp - 1; i++)
            {
                pow = pow * me;
            }

            return pow;
        }

        /*
        public static BigInteger ModInverse(this BigInteger me, BigInteger modulus)
        {
            BigInteger bint = new BigInteger(me.ToByteArray());
            BigInteger modInverse = bint.modInverse(new BigInteger(modulus.ToByteArray()));
            return BigInteger.Parse(modInverse.ToString());
        }
        */
    }
}
