﻿using Aero.Cryptography.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aero.Cryptography.Contracts
{
    public interface ISignatureKey
    {
        BigInteger SignatureKey { get; }
    }
}
