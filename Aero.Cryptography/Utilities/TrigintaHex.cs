using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aero.Cryptography.Utilities
{
    public class TrigintaHex
    {
        #region Constants

        private static readonly Dictionary<char, byte> CharValueDictionary = new Dictionary<char, byte>()
        {
            { '0', 0 },
            { '1', 1 },
            { '2', 2 },
            { '3', 3 },
            { '4', 4 },
            { '5', 5 },
            { '6', 6 },
            { '7', 7 },
            { '8', 8 },
            { '9', 9 },

            { 'A', 10 },
            { 'B', 11 },
            { 'C', 12 },
            { 'D', 13 },
            { 'E', 14 },
            { 'F', 15 },
            { 'G', 16 },
            { 'H', 17 },
            { 'I', 18 },
            { 'J', 19 },
            { 'K', 20 },
            { 'L', 21 },
            { 'M', 22 },
            { 'N', 23 },
            { 'O', 24 },
            { 'P', 25 },
            { 'Q', 26 },
            { 'R', 27 },
            { 'S', 28 },
            { 'T', 29 },
            { 'U', 30 },
            { 'V', 31 },
            { 'W', 32 },
            { 'X', 33 },
            { 'Y', 34 },
            { 'Z', 35 },
        };

        #endregion

        #region Fields

        private BigInteger value;

        #endregion

        #region Properties

        private string StringValue { get; set; }

        public BigInteger Value
        {
            get
            {
                return value;
            }

            private set
            {
                this.value = value;
                this.StringValue = GetTrigintahexString(value);
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Zero constructor
        /// </summary>
        public TrigintaHex()
        {
            this.Value = new BigInteger();
        }

        /// <summary>
        /// Decimal constructor
        /// </summary>
        public TrigintaHex(long dec)
        {
            this.Value = new BigInteger(dec);
        }

        /// <summary>
        /// Decimal constructor
        /// </summary>
        public TrigintaHex(BigInteger dec)
        {
            this.Value = dec;
        }

        #endregion

        #region Methods

        public static TrigintaHex Parse(string trigintahex)
        {
            trigintahex = System.Text.RegularExpressions.Regex.Replace(trigintahex, @"\s+", "");

            BigInteger result = new BigInteger();
            BigInteger base36 = new BigInteger(36);

            trigintahex = trigintahex.ToUpper();

            for (int i = 0; i < trigintahex.Length; i++)
            {
                char c = trigintahex[trigintahex.Length - i - 1];

                if (CharValueDictionary.ContainsKey(c))
                {
                    BigInteger partValue = new BigInteger(CharValueDictionary[c]) * base36.Pow(i);
                    result += partValue;
                }
                else
                    throw new Exception("FormatException!");
            }

            return new TrigintaHex(result);
        }

        public static bool TryParse(string strValue, out TrigintaHex trigintahex)
        {
            try
            {
                trigintahex = Parse(strValue);
                return true;
            }
            catch
            {
                trigintahex = new TrigintaHex();
                return false;
            }
        }

        private static string GetTrigintahexString(BigInteger dec)
        {
            BigInteger divided = dec;
            BigInteger division;
            BigInteger remainder;

            remainder = divided % 36;
            division = (divided - remainder) / 36;

            if (division > 36)
            {
                byte modByte = byte.Parse(remainder.ToString());
                return GetTrigintahexString(division) + CharValueDictionary.FirstOrDefault(x => x.Value == modByte).Key;
            }
            else
            {
                byte divisionByte = byte.Parse(division.ToString());
                byte remainderByte = byte.Parse(remainder.ToString());

                return CharValueDictionary.FirstOrDefault(x => x.Value == divisionByte).Key + "" + CharValueDictionary.FirstOrDefault(x => x.Value == remainderByte).Key;
            }
        }

        public override string ToString()
        {
            return this.StringValue;
        }

        public string ToString(int partLength)
        {
            if (string.IsNullOrEmpty(this.StringValue))
                return string.Empty;

            if (partLength < 0 || partLength >= this.StringValue.Length)
                return this.StringValue;

            string result = string.Empty;

            int length = this.StringValue.Length - (this.StringValue.Length % partLength);

            for (int i = 0; i < length; i += partLength)
                result += this.StringValue.Substring(i, partLength) + " ";

            if (this.StringValue.Length > length)
                result += this.StringValue.Substring(length, this.StringValue.Length - length);

            if (!string.IsNullOrEmpty(result) && result.LastOrDefault() == ' ')
                result = result.Substring(0, result.Length - 1);


            return result;
        }

        #endregion
    }
}
