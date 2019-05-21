// Filename: BestOf.cs
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
    public class BestOf : TypeObject<BestOf, int>
    {
        public const int Min = 1;
        public const int Max = 7;
        public const int Default = 3;

        public static readonly BestOf MinValue = new BestOf(Min);
        public static readonly BestOf MaxValue = new BestOf(Max);
        public static readonly BestOf DefaultValue = new BestOf(Default);

        public BestOf(int bestOf, bool validate = true) : base(bestOf)
        {
            if (validate)
            {
                if (bestOf < Min || bestOf > Max)
                {
                    throw new ArgumentException(nameof(bestOf));
                }
            }
        }
    }
}
