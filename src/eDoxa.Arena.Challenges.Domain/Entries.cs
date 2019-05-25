// Filename: Entries.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Arena.Challenges.Domain
{
    public class Entries : TypeObject<Entries, int>
    {
        public Entries(PayoutEntries payoutEntries, PayoutRatio payoutRatio) : base(Convert.ToInt32(payoutEntries / payoutRatio))
        {
        }

        internal Entries(int entries) : base(entries)
        {
        }
    }
}
