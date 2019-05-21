﻿// Filename: Leftover.cs
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
    public class Leftover : TypeObject<Leftover, decimal>
    {
        internal const decimal Default = 0;

        internal static readonly Leftover DefaultValue = new Leftover(Default);

        public Leftover(decimal leftover) : base(leftover)
        {
        }

        public override string ToString()
        {
            return Value.ToString(CultureInfo.InvariantCulture);
        }
    }
}
