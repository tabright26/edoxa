// Filename: StatWeighting.cs
// Date Created: 2019-04-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using JetBrains.Annotations;

namespace eDoxa.Challenges.Domain.Entities.AggregateModels.MatchAggregate
{
    public partial class StatWeighting
    {
        private readonly float _value;

        public StatWeighting(float weighting)
        {
            _value = weighting;
        }

        public static implicit operator float(StatWeighting weighting)
        {
            return weighting._value;
        }

        public override string ToString()
        {
            return _value.ToString("R");
        }
    }

    public partial class StatWeighting : IEquatable<StatWeighting>
    {
        public bool Equals(StatWeighting other)
        {
            return _value.Equals(other?._value);
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as StatWeighting);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
    }

    public partial class StatWeighting : IComparable, IComparable<StatWeighting>
    {
        public int CompareTo([CanBeNull] object obj)
        {
            return this.CompareTo(obj as StatWeighting);
        }

        public int CompareTo([CanBeNull] StatWeighting other)
        {
            return _value.CompareTo(other?._value);
        }        
    }
}