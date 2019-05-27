// Filename: EntryFee.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Seedwork.Domain.Aggregate;
using eDoxa.Seedwork.Domain.Enumerations;

using JetBrains.Annotations;

namespace eDoxa.Arena.Domain
{
    public class EntryFee : TypedObject<EntryFee, decimal>
    {
        private Currency _currency;

        public EntryFee(decimal amount, Currency currency)
        {
            Value = amount;
            _currency = currency;
        }

        public Currency Currency => _currency;

        public override bool Equals([CanBeNull] EntryFee other)
        {
            return Currency.Equals(other?.Currency) && base.Equals(other);
        }
    }
}
