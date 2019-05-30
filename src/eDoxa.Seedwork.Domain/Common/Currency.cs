// Filename: Currency.cs
// Date Created: 2019-05-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

using eDoxa.Seedwork.Domain.Aggregate;
using eDoxa.Seedwork.Domain.Common.Enumerations;

namespace eDoxa.Seedwork.Domain.Common
{
    public abstract class Currency : ValueObject
    {
        protected Currency(CurrencyType type, decimal amount)
        {
            Type = type;
            Amount = amount;
        }

        public CurrencyType Type { get; private set; }

        public decimal Amount { get; private set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Type;
            yield return Amount;
        }
    }
}
