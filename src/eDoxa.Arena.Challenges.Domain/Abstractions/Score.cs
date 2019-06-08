// Filename: Score.cs
// Date Created: 2019-06-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;
using System.Globalization;

using eDoxa.Seedwork.Domain.Aggregate;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Domain.Abstractions
{
    public abstract class Score : ValueObject, IComparable
    {
        protected Score(decimal score)
        {
            Value = Math.Round(score, 2);
        }

        public decimal Value { get; private set; }

        public int CompareTo([CanBeNull] object obj)
        {
            return Value.CompareTo(((Score) obj)?.Value);
        }

        public static implicit operator decimal(Score score)
        {
            return score.Value;
        }

        public override string ToString()
        {
            return Value.ToString(CultureInfo.InvariantCulture);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
