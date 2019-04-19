﻿// Filename: PrizePool.cs
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
    public partial class PrizePool : ValueObject
    {
        private readonly decimal _prizePool;

        public PrizePool(Entries entries, EntryFee entryFee, ServiceChargeRatio serviceChargeRatio)
        {
            _prizePool = Math.Floor(entries.ToInt32() * entryFee.ToDecimal() * (1 - Convert.ToDecimal(serviceChargeRatio.ToSingle())));
        }

        public decimal ToDecimal()
        {
            return _prizePool;
        }
    }

    public partial class PrizePool : IComparable, IComparable<PrizePool>
    {
        public int CompareTo([CanBeNull] object obj)
        {
            return this.CompareTo(obj as PrizePool);
        }

        public int CompareTo([CanBeNull] PrizePool other)
        {
            return _prizePool.CompareTo(other?._prizePool);
        }
    }
}