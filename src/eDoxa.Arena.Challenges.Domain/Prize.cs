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

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Domain
{
    public class Prize : TypeObject<Prize, decimal>
    {
        public static readonly Prize None = new Prize(0);

        public Prize(decimal prize) : base(prize)
        {
            if (prize < 0)
            {
                throw new ArgumentException(nameof(prize));
            }
        }

        protected Prize(Prize prize, PayoutFactor factor) : base(prize * factor)
        {
        }

        protected virtual PrizeType Type => PrizeType.All;

        public override bool Equals([CanBeNull] Prize obj)
        {
            return Type.Equals(obj?.Type) && base.Equals(obj);
        }

        public override string ToString()
        {
            return Value.ToString(CultureInfo.InvariantCulture);
        }

        public Prize ApplyFactor(PayoutFactor factor)
        {
            return factor is MoneyPayoutFactor ? new MoneyPrize(this, factor) :
                factor is TokenPayoutFactor ? new TokenPrize(this, factor) :
                new Prize(this, factor);
        }
    }
}
