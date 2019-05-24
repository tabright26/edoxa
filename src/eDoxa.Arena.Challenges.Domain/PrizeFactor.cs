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

namespace eDoxa.Arena.Challenges.Domain
{
    public sealed class PrizeFactor : Prize
    {
        public PrizeFactor(decimal factor) : base(factor, Currency.Undefined)
        {
            if (factor < 1)
            {
                throw new ArgumentException(nameof(factor));
            }
        }

        public Prize GetPrize(EntryFee entryFee, Currency currency)
        {
            if (currency == Currency.Money)
            {
                return new MoneyPrize(Amount * entryFee);
            }

            if (currency == Currency.Token)
            {
                return new TokenPrize(Amount * entryFee);
            }

            throw new ArgumentException(nameof(currency));
        }
    }
}
