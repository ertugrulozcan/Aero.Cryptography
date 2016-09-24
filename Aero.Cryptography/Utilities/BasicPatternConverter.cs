using Aero.Cryptography.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aero.Cryptography.Utilities
{
    public class BasicPatternConverter : IPatternConverter
    {
        #region Fields

        private static BasicPatternConverter current;

        #endregion

        #region Properties

        public static BasicPatternConverter Current
        {
            get
            {
                if (current == null)
                    new BasicPatternConverter();

                return current;
            }
            private set
            {
                current = value;
            }
        }
        
        public Encoding EncodingProcedure
        {
            get
            {
                return Encoding.Unicode;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        private BasicPatternConverter()
        {
            BasicPatternConverter.Current = this;
        }

        #endregion

        #region Methods

        public BigInteger ConvertToBigInteger(byte[] bytes)
        {
            if (bytes == null)
                return 0;

            string bigStr = string.Empty;

            foreach (byte no in bytes)
            {
                if (no < 10)
                    bigStr += "00" + no;
                else if (no < 100)
                    bigStr += "0" + no;
                else
                    bigStr += no;
            }

            bigStr = "1" + bigStr;

            return new BigInteger(bigStr, 10);
        }

        // 1 084 000 101 000 115 000 116 000
        // 1 187 022 (5819)

        public byte[] ConvertToByteArray(BigInteger number)
        {
            List<byte> byteArray = new List<byte>();

            string bigStr = number.ToString();
            bigStr = bigStr.Substring(1, bigStr.Length - 1);

            int length = bigStr.Length - bigStr.Length % 3;
            for (int i = 0; i < length; i += 3)
            {
                string part = bigStr.Substring(i, 3);
                byteArray.Add(this.ParseToByte(part));
            }

            if(length < bigStr.Length)
            {
                string part = bigStr.Substring(length, bigStr.Length - length);
                byteArray.Add(this.ParseToByte(part));
            }

            return byteArray.ToArray();
        }

        private byte ParseToByte(string str)
        {
            byte bytePart;
            bool isParsed = byte.TryParse(str, out bytePart);

            if (isParsed)
                return bytePart;
            else
                return 0;
        }

        #endregion
    }
}

/*

*/
