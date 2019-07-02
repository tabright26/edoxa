// Filename: Score.cs
// Date Created: 2019-06-25
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

namespace eDoxa.Arena.Challenges.Domain.AggregateModels
{
    public abstract partial class Score : ValueObject
    {
        private readonly decimal _score;

        protected Score(decimal score)
        {
            _score = score;
        }

        public override string ToString()
        {
            return _score.ToString(CultureInfo.InvariantCulture);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return _score;
        }

        public decimal ToDecimal()
        {
            return _score;
        }
    }

    public abstract partial class Score : IComparable, IComparable<Score>
    {
        public int CompareTo([CanBeNull] object obj)
        {
            return this.CompareTo(obj as Score);
        }

        public int CompareTo([CanBeNull] Score other)
        {
            return _score.CompareTo(other?._score);
        }
    }
}
