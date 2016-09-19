using Aero.Cryptography.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aero.Cryptography.Algorithms.Contracts
{
    public interface IPatternConverter
    {
        Encoding EncodingProcedure { get; }
        byte[] ConvertToByteArray(BigInteger number);
        BigInteger ConvertToBigInteger(byte[] text);
    }
}
