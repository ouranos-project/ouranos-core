﻿namespace Martiscoin.NBitcoin.BouncyCastle.math.field
{
    internal interface IPolynomialExtensionField
        : IExtensionField
    {
        IPolynomial MinimalPolynomial
        {
            get;
        }
    }
}
