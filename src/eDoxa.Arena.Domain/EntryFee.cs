﻿// Filename: EntryFee.cs
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
        public EntryFee(decimal amount, CurrencyType currencyType)
        {
            Amount = amount;
            CurrencyType = currencyType;
        }

        public decimal Amount { get; private set; }

        public CurrencyType CurrencyType { get; private set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Amount;
            yield return CurrencyType;
        }

        public override string ToString()
        {
            return Amount.ToString(CultureInfo.InvariantCulture);
        }
    }
}
