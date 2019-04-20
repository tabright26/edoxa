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

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate.Helpers;
using eDoxa.Seedwork.Domain.Aggregate;

using JetBrains.Annotations;

namespace eDoxa.Challenges.Domain.ValueObjects
{
    public partial class EntryFee : ValueObject
    {
        internal const decimal Min = 0.25M;
        internal const decimal Max = 1500M;
        internal const decimal Default = 5M;

        public static readonly EntryFee MinValue = new EntryFee(Min);
        public static readonly EntryFee MaxValue = new EntryFee(Max);
        public static readonly EntryFee DefaultValue = new EntryFee(Default);

        private readonly decimal _value;

        public EntryFee(decimal entryFee, bool validate = true)
        {
            if (validate)
            {
                if (entryFee < Min ||
                    entryFee > Max ||
                    entryFee % 0.25M != 0)
                {
                    throw new ArgumentException(nameof(entryFee));
                }
            }

            _value = entryFee;
        }

        public static implicit operator decimal(EntryFee entryFee)
        {
            return entryFee._value;
        }

        public static EntryFee Random(EntryFee min, EntryFee max)
        {
            if (min > max)
            {
                throw new ArgumentException(nameof(min));
            }

            var random = new Random();

            const int multiplierOfCents = 100;

            var minEntryFee = (int) Math.Round(min * multiplierOfCents);

            var maxEntryFee = (int) Math.Round(max * multiplierOfCents);

            var entryFee = random.Next(minEntryFee, maxEntryFee + 1);

            // Entry fee is under 5$.
            if (entryFee < 5 * multiplierOfCents)
            {
                entryFee = Optimization.RoundMultiplier(entryFee, multiplierOfCents);

                if (entryFee == 0)
                {
                    return new EntryFee(0.25M);
                }

                if (entryFee * multiplierOfCents >= maxEntryFee)
                {
                    return new EntryFee(entryFee);
                }

                var cents = random.Next(0, 4) * 0.25M;

                return new EntryFee(entryFee + cents);
            }

            var multiplier = 1 * multiplierOfCents;

            // Entry fee is over 5$
            if (entryFee >= 5 * multiplierOfCents)
            {
                multiplier = 5 * multiplierOfCents;
            }

            // Entry fee is over 100$
            if (entryFee >= 100 * multiplierOfCents)
            {
                multiplier = 50 * multiplierOfCents;
            }

            // Entry fee is over 1000$
            if (entryFee >= 1000 * multiplierOfCents)
            {
                multiplier = 500 * multiplierOfCents;
            }

            entryFee = Optimization.RoundMultiplier(entryFee, multiplier);

            return new EntryFee(entryFee * (multiplier / multiplierOfCents));
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
            return _value.CompareTo(other?._value);
        }
    }
}