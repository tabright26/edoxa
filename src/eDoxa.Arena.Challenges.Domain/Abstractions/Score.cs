﻿// Filename: Score.cs
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
        private readonly decimal _score;

        protected Score(decimal score)
        {
            _score = score;
        }

        public int CompareTo([CanBeNull] object obj)
        {
            return _score.CompareTo(((Score) obj)?._score);
        }

        public static implicit operator decimal(Score score)
        {
            return score._score;
        }

        public override string ToString()
        {
            return _score.ToString(CultureInfo.InvariantCulture);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return _score;
        }
    }
}
