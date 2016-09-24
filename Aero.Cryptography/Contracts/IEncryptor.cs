using Aero.Cryptography.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aero.Cryptography.Contracts
{
    public interface IEncryptor
    {
        ISecret Encrypt(byte[] obj);
        ISecret Encrypt(byte[] obj, IPublicKey publicKey);
        IPatternConverter PatternConverter { get; }
    }
}
