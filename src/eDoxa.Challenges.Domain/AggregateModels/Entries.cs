// Filename: Entries.cs
// Date Created: 2019-04-20
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

namespace eDoxa.Challenges.Domain.AggregateModels
{
    public partial class Entries : ValueObject
    {
        internal const int Min = 30;
        internal const int Max = 2500;
        internal const int Default = 50;

        public static readonly Entries MinValue = new Entries(Min);
        public static readonly Entries MaxValue = new Entries(Max);
        public static readonly Entries DefaultValue = new Entries(Default);

        private readonly int _value;

        public Entries(int entries, bool validate = true)
        {
            if (validate)
            {
                if (entries < Min ||
                    entries > Max ||
                    entries % 10 != 0)
                {
                    throw new ArgumentException(nameof(entries));
                }
            }

            _value = entries;
        }

        public static implicit operator int(Entries entries)
        {
            return entries._value;
        }

        public override string ToString()
        {
            return _value.ToString();
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
            return _value.CompareTo(other?._value);
        }
    }
}