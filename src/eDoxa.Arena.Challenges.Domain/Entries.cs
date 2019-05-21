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
        public const int Min = 30;
        public const int Max = 2500;
        public const int Default = 50;

        public static readonly Entries MinValue = new Entries(Min);
        public static readonly Entries MaxValue = new Entries(Max);
        public static readonly Entries DefaultValue = new Entries(Default);

        public Entries(int entries, bool validate = true) : base(entries)
        {
            if (validate)
            {
                if (entries < Min || entries > Max || entries % 10 != 0)
                {
                    throw new ArgumentException(nameof(entries));
                }
            }
        }
    }
}
