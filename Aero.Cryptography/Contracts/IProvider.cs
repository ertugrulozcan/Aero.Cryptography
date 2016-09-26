using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aero.Cryptography.Contracts
{
    interface IProvider
    {
        IEncryptor Encoder { get; }
        IDecryptor Decoder { get; }
        ISigner Signer { get; }
        string Algorithm { get; }
    }
}
