// Filename: Score.cs
// Date Created: 2019-05-20
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

namespace eDoxa.Arena.Domain.Abstractions
{
    public abstract class Score : ValueObject
    {
        protected Score(decimal score)
        {
            Value = Math.Round(score, 2);
        }

        public decimal Value { get; private set; }

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
