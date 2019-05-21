// Filename: EntryFee.cs
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
    public class EntryFee : TypeObject<EntryFee, decimal>
    {
        public const decimal Min = 0.25M;
        public const decimal Max = 1500M;
        public const decimal Default = 5M;

        public static readonly EntryFee MinValue = new EntryFee(Min);
        public static readonly EntryFee MaxValue = new EntryFee(Max);
        public static readonly EntryFee DefaultValue = new EntryFee(Default);

        public EntryFee(decimal entryFee, bool validate = true) : base(entryFee)
        {
            if (validate)
            {
                if (entryFee < Min || entryFee > Max || entryFee % 0.25M != 0)
                {
                    throw new ArgumentException(nameof(entryFee));
                }
            }
        }

        public override string ToString()
        {
            return Value.ToString(CultureInfo.InvariantCulture);
        }
    }
}
