// Filename: PrizeFactor.cs
// Date Created: 2019-05-23
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Arena.Domain
{
    public sealed class PrizeFactor : ValueObject
    {
        public PrizeFactor(decimal factor)
        {
            if (factor < 1)
            {
                throw new ArgumentException(nameof(factor));
            }

            Value = factor;
        }

        public decimal Value { get; private set; }

        public static implicit operator decimal(PrizeFactor prizeFactor)
        {
            return prizeFactor.Value;
        }

        public override string ToString()
        {
            return Value.ToString("F1");
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
