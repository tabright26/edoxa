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

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Arena.Domain
{
    public sealed class PrizeFactor : TypeObject<PrizeFactor, decimal>
    {
        public PrizeFactor(decimal factor) : base(factor)
        {
            if (factor < 1)
            {
                throw new ArgumentException(nameof(factor));
            }
        }
    }
}
