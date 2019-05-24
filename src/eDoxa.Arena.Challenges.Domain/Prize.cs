// Filename: Prize.cs
// Date Created: 2019-05-23
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
    public abstract class Prize : ValueObject
    {
        public static readonly Prize None = new UndefinedPrize();

        private readonly decimal _amount;
        private readonly Currency _currency;

        protected Prize(decimal amount, Currency currency)
        {
            if (amount < 0)
            {
                throw new ArgumentException(nameof(amount));
            }

            _amount = amount;
            _currency = currency;
        }

        public decimal Amount => _amount;

        public Currency Currency => _currency;

        public static implicit operator decimal(Prize prize)
        {
            return prize.Amount;
        }

        public override string ToString()
        {
            return Amount.ToString(CultureInfo.InvariantCulture);
        }
    }
}
