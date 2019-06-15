// Filename: PayoutEntries.cs
// Date Created: 2019-06-12
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;

using eDoxa.Seedwork.Domain.Aggregate;
using eDoxa.Seedwork.Domain.Attributes;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate.ValueObjects
{
    public sealed class PayoutEntries : ValueObject
    {
        [AllowValue(false)] public static readonly PayoutEntries One = new PayoutEntries(1);
        [AllowValue(false)] public static readonly PayoutEntries Two = new PayoutEntries(2);
        [AllowValue(false)] public static readonly PayoutEntries Three = new PayoutEntries(3);
        [AllowValue(false)] public static readonly PayoutEntries Four = new PayoutEntries(4);
        [AllowValue(true)] public static readonly PayoutEntries Five = new PayoutEntries(5);
        [AllowValue(true)] public static readonly PayoutEntries Ten = new PayoutEntries(10);
        [AllowValue(true)] public static readonly PayoutEntries Fifteen = new PayoutEntries(15);
        [AllowValue(true)] public static readonly PayoutEntries Twenty = new PayoutEntries(20);
        [AllowValue(true)] public static readonly PayoutEntries TwentyFive = new PayoutEntries(25);
        [AllowValue(true)] public static readonly PayoutEntries Fifty = new PayoutEntries(50);
        [AllowValue(false)] public static readonly PayoutEntries SeventyFive = new PayoutEntries(75);
        [AllowValue(false)] public static readonly PayoutEntries OneHundred = new PayoutEntries(100);

        public PayoutEntries(Entries entries, PayoutRatio payoutRatio) : this()
        {
            Value = Convert.ToInt32(Math.Floor(entries * payoutRatio));
        }

        public PayoutEntries(int payoutEntries) : this()
        {
            Value = payoutEntries;
        }

        private PayoutEntries()
        {
            // Required by EF Core.
        }

        public int Value { get; private set; }

        public static implicit operator int(PayoutEntries payoutEntries)
        {
            return payoutEntries.Value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
