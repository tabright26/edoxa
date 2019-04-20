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

using JetBrains.Annotations;

namespace eDoxa.Challenges.Domain.ValueObjects
{
    public partial class Entries : ValueObject
    {
        internal const int MinEntries = 30;
        internal const int MaxEntries = 2000;
        internal const int DefaultPrimitive = 50;

        public static readonly Entries Default = new Entries(DefaultPrimitive);

        private readonly int _entries;

        public Entries(int entries, bool validate = true)
        {
            if (validate)
            {
                if (entries < MinEntries ||
                    entries > MaxEntries ||
                    entries % 10 != 0)
                {
                    throw new ArgumentException(nameof(entries));
                }
            }

            _entries = entries;
        }

        public static implicit operator int(Entries entries)
        {
            return entries._entries;
        }
    }

    public partial class Entries : IComparable, IComparable<Entries>
    {
        public int CompareTo([CanBeNull] object obj)
        {
            return this.CompareTo(obj as Entries);
        }

        public int CompareTo([CanBeNull] Entries other)
        {
            return _entries.CompareTo(other?._entries);
        }
    }
}