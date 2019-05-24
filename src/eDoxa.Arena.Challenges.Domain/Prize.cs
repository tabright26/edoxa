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
    public class Prize : ValueObject
    {
        public static readonly Prize None = new Prize(0, Currency.Undefined);

        private readonly decimal _amount;
        private readonly Currency _type;

        public Prize(decimal amount, Currency type)
        {
            if (amount < 0)
            {
                throw new ArgumentException(nameof(amount));
            }

            _amount = amount;
            _type = type;
        }

        public decimal Amount => _amount;

        public Currency Type => _type;

        public static implicit operator decimal(Prize prize)
        {
            return prize.Amount;
        }

        public override string ToString()
        {
            return Amount.ToString(CultureInfo.InvariantCulture);
        }

        public Prize ApplyEntryFee(EntryFee entryFee, Currency currency)
        {
            return new Prize(Amount * entryFee, currency);
        }
    }
}
