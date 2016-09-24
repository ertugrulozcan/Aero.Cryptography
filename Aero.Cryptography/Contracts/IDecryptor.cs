using Aero.Cryptography.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aero.Cryptography.Contracts
{
    public interface IDecryptor
    {
        byte[] Decrypt(ISecret crypto);
        byte[] Decrypt(ISecret crypto, IPrivateKey privateKey);
        IPatternConverter PatternConverter { get; }
    }
}
