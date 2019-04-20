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

using JetBrains.Annotations;

namespace eDoxa.Challenges.Domain.ValueObjects
{
    public partial class EntryFee : ValueObject
    {
        internal const decimal MinEntryFee = 0.25M;
        internal const decimal MaxEntryFee = 1500M;
        internal const decimal DefaultPrimitive = 5M;

        public static readonly EntryFee Default = new EntryFee(DefaultPrimitive);

        private readonly decimal _entryFee;

        public EntryFee(decimal entryFee, bool validate = true)
        {
            if (validate)
            {
                if (entryFee < MinEntryFee ||
                    entryFee > MaxEntryFee ||
                    entryFee % 0.25M != 0)
                {
                    throw new ArgumentException(nameof(entryFee));
                }
            }

            _entryFee = entryFee;
        }

        public static implicit operator decimal(EntryFee entryFee)
        {
            return entryFee._entryFee;
        }
    }

    public partial class EntryFee : IComparable, IComparable<EntryFee>
    {
        public int CompareTo([CanBeNull] object obj)
        {
            return this.CompareTo(obj as EntryFee);
        }

        public int CompareTo([CanBeNull] EntryFee other)
        {
            return _entryFee.CompareTo(other?._entryFee);
        }
    }
}