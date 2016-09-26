using Aero.Cryptography.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aero.Cryptography.Contracts
{
    public interface ISecret
    {
        BigInteger Value { get; }
        
        byte[] Sign { get; set; }

        string SignAlgorithm { get; set; }

        string Serialize();
        void Deserialize(string jsonStr);
    }
}
