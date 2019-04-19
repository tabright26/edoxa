// Filename: Entries.cs
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
    public class Entries : ValueObject
    {
        internal const int MinEntries = 30;
        internal const int MaxEntries = 2000;

        public static readonly Entries Default = new Entries(50);

        private readonly int _entries;

        public Entries(int entries)
        {
            if (entries < MinEntries ||
                entries > MaxEntries ||
                entries % 10 != 0)
            {
                throw new ArgumentException(nameof(entries));
            }

            _entries = entries;
        }

        public int ToInt32()
        {
            return _entries;
        }
    }
}