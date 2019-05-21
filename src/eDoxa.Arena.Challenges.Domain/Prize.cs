// Filename: Prize.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Globalization;

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Arena.Challenges.Domain
{
    public abstract class Prize : TypeObject<Prize, decimal>
    {
        protected Prize(decimal prize) : base(prize)
        {
            if (prize < 0)
            {
                throw new ArgumentException(nameof(prize));
            }
        }

        protected Prize(FirstPrize prize) : base(prize)
        {
        }

        public override string ToString()
        {
            return Value.ToString(CultureInfo.InvariantCulture);
        }
    }
}
