// Filename: EntryFee.cs
// Date Created: 2019-04-19
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Challenges.Domain.ValueObjects
{
    public class EntryFee : ValueObject
    {
        internal const decimal MinEntryFee = 0.25M;
        internal const decimal MaxEntryFee = 1500M;

        private static readonly EntryFee Default = new EntryFee(5M);

        private readonly decimal _entryFee;

        public EntryFee(decimal entryFee)
        {
            if (entryFee < MinEntryFee ||
                entryFee > MaxEntryFee ||
                entryFee % 0.25M != 0)
            {
                throw new ArgumentException(nameof(entryFee));
            }

            _entryFee = entryFee;
        }

        public decimal ToDecimal()
        {
            return _entryFee;
        }
    }
}