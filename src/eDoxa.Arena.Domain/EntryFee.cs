// Filename: EntryFee.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Globalization;

using eDoxa.Seedwork.Domain.Aggregate;
using eDoxa.Seedwork.Domain.Common.Enumerations;

namespace eDoxa.Arena.Domain
{
    public class EntryFee : ValueObject
    {
        public EntryFee(decimal amount, Currency currency)
        {
            Amount = amount;
            Currency = currency;
        }

        public decimal Amount { get; private set; }

        public Currency Currency { get; private set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Amount;
            yield return Currency;
        }

        public override string ToString()
        {
            return Amount.ToString(CultureInfo.InvariantCulture);
        }
    }
}
