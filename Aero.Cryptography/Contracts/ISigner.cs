using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aero.Cryptography.Contracts
{
    public interface ISigner
    {
        ISignatureKey SignatureKey { get; }
        ISignatureKey VerificationKey { get; }

        string SignatureAlgorithm { get; }

        // Hash
        byte[] Hash(byte[] data);

        // Sign
        byte[] Sign(byte[] data);
        byte[] Sign(byte[] data, ISignatureKey signatureKey);

        // Verify
        bool Verify(byte[] data, byte[] sign);
        bool Verify(byte[] data, byte[] sign, ISignatureKey verificationKey);
    }
}
