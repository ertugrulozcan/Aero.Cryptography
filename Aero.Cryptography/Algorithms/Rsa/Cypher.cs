using Aero.Cryptography.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aero.Cryptography.Utilities;
using System.Xml;

namespace Aero.Cryptography.Algorithms.Rsa
{
    public class Cypher : ISecret
    {
        public readonly string CryptoAlgorithm;
        public string SignAlgorithm { get; set; }

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
        
        public byte[] Sign { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Cypher()
        { }

        /// <summary>
        /// Constructor
        /// </summary>
        public Cypher(BigInteger value, byte[] sign = null, string cryptoAlgorithm = "", string signAlgorithm = "")
        {
            this.TrigintaHexValue = new TrigintaHex(value);
            this.Sign = sign;

            this.CryptoAlgorithm = cryptoAlgorithm;
            this.SignAlgorithm = signAlgorithm;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public Cypher(string str, byte[] sign = null, string cryptoAlgorithm = "", string signAlgorithm = "")
        {
            this.TrigintaHexValue = TrigintaHex.Parse(str);
            this.Sign = sign;

            this.CryptoAlgorithm = cryptoAlgorithm;
            this.SignAlgorithm = signAlgorithm;
        }
        
        public string Serialize()
        {
            string cert = string.Empty;

            string cypher = this.TrigintaHexValue.ToString(4, 10);

            cert += this.PrettifyXmlRow("<secret>", 0);
            cert += this.PrettifyXmlRow("<cypher length=\"" + cypher.Length + "\"" + " algorithm=\"" + this.CryptoAlgorithm + "\">", 1);
            cert += this.PrettifyXmlRow(cypher, 2);
            cert += this.PrettifyXmlRow("</cypher>", 1);

            string sign = (this.Sign != null ? new BigInteger(this.Sign).ToString(16) : string.Empty);
            
            cert += this.PrettifyXmlRow("<sign length=\"" + sign.Length + "\"" + " algorithm=\"" + this.SignAlgorithm + "\">", 1);
            cert += this.PrettifyXmlRow(sign, 2);
            cert += this.PrettifyXmlRow("</sign>", 1);

            cert += this.PrettifyXmlRow("</secret>", 0);

            return cert;
        }

        private string PrettifyXmlRow(string rowStr, int indentLevel)
        {
            string tabs = string.Empty;
            for (int i = 0; i < indentLevel; i++)
                tabs += "\t";

            return tabs + rowStr.Replace(Environment.NewLine, Environment.NewLine + tabs) + Environment.NewLine;
        }

        public void Deserialize(string xmlStr)
        {
            try
            {
                string cypherStr = null;
                string signStr = null;
                
                XmlTextReader xmlReader = new XmlTextReader(new System.IO.StringReader(xmlStr));
                string pivotElement = null;
                while (xmlReader.Read())
                {
                    switch (xmlReader.NodeType)
                    {
                        case XmlNodeType.Element:
                            {
                                pivotElement = xmlReader.Name;
                            }
                            break;
                        case XmlNodeType.Text:
                            {
                                if (pivotElement == "cypher")
                                    cypherStr = xmlReader.Value;
                                else if (pivotElement == "sign")
                                    signStr = xmlReader.Value;
                            }
                            break;
                    }
                }

                byte[] sign = null;
                if (!string.IsNullOrEmpty(signStr))
                    sign = new BigInteger(signStr, 16).getBytes();

                Cypher cypher = new Cypher(new BigInteger(TrigintaHex.Parse(cypherStr).Value), sign);
                this.TrigintaHexValue = cypher.TrigintaHexValue;
                this.Sign = cypher.Sign;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
    }
}