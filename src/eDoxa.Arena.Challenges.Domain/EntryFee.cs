// Filename: EntryFee.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Globalization;

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Arena.Challenges.Domain
{
    public class EntryFee : ValueObject
    {
        private decimal _amount;
        private Currency _currency;

        internal EntryFee(decimal amount, Currency currency)
        {
            _amount = amount;
            _currency = currency;
        }

        public decimal Amount => _amount;

        public Currency Currency => _currency;

        public static implicit operator decimal(EntryFee entryFee)
        {
            return entryFee.Amount;
        }

        public override string ToString()
        {
            return Amount.ToString(CultureInfo.InvariantCulture);
        }
    }
}
