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

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate.Helpers;
using eDoxa.Seedwork.Domain.Aggregate;

using JetBrains.Annotations;

namespace eDoxa.Challenges.Domain.ValueObjects
{
    public partial class Entries : ValueObject
    {
        internal const int Min = 30;
        internal const int Max = 2000;
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

        public static Entries Random(Entries minValue, Entries maxValue)
        {
            if (minValue > maxValue)
            {
                throw new ArgumentException(nameof(minValue));
            }

            // Entries is under 100$
            var multiplier = 10;

            // Entries is over 100$
            if (minValue >= 100)
            {
                multiplier = 50;
            }

            // Entries is over 1000$
            if (minValue >= 1000)
            {
                multiplier = 500;
            }

            var random = new Random();

            var entries = random.Next(minValue, maxValue + 1);

            entries = Optimization.RoundMultiplier(entries, multiplier);

            return new Entries(entries * multiplier);
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