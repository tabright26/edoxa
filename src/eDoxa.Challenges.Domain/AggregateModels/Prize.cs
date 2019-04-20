// Filename: Prize.cs
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

namespace eDoxa.Challenges.Domain.AggregateModels
{
    public partial class Prize : ValueObject
    {
        private readonly decimal _value;

        public Prize(decimal prize)
        {
            if (prize < 0)
            {
                throw new ArgumentException(nameof(prize));
            }

            _value = prize;
        }

        protected Prize(WinnerPrize prize)
        {
            _value = prize;
        }

        public static implicit operator decimal(Prize prize)
        {
            return prize._value;
        }
    }

    public partial class Prize : IComparable, IComparable<Prize>
    {
        public int CompareTo([CanBeNull] object obj)
        {
            return this.CompareTo(obj as Prize);
        }

        public int CompareTo([CanBeNull] Prize other)
        {
            return _value.CompareTo(other?._value);
        }
    }
}