﻿// Filename: Score.cs
// Date Created: 2019-04-19
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Globalization;

using JetBrains.Annotations;

namespace eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public partial class Score
    {
        private readonly decimal _value;

        internal Score(decimal score)
        {
            _value = Math.Round(score, 2);
        }

        public static implicit operator decimal(Score score)
        {
            return score._value;
        }

        public override string ToString()
        {
            return _value.ToString(CultureInfo.InvariantCulture);
        }
    }

    public partial class Score : IEquatable<Score>
    {
        public bool Equals([CanBeNull] Score other)
        {
            return _value.Equals(other?._value);
        }

        public override bool Equals([CanBeNull] object obj)
        {
            return this.Equals(obj as Score);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
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
            return _value.CompareTo(other?._value);
        }
    }
}