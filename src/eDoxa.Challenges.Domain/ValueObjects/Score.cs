// Filename: Score.cs
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
    public partial class Score : ValueObject
    {
        private readonly decimal _score;

        internal Score(decimal score)
        {
            _score = Math.Round(score, 2);
        }

        public static implicit operator decimal(Score score)
        {
            return score._score;
        }
    }

    public partial class Score : IComparable, IComparable<Score>
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