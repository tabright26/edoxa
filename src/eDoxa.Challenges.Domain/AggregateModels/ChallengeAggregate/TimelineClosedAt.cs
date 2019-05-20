// Filename: TimelineClosedAt.cs
// Date Created: 2019-04-22
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using JetBrains.Annotations;

namespace eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public sealed partial class TimelineClosedAt
    {
        private readonly DateTime _value;

        public TimelineClosedAt()
        {
            _value = DateTime.UtcNow;
        }

        internal TimelineClosedAt(DateTime closedAt)
        {
            _value = closedAt;
        }

        public static implicit operator DateTime(TimelineClosedAt closedAt)
        {
            return closedAt._value;
        }
    }

    public sealed partial class TimelineClosedAt : IEquatable<TimelineClosedAt>
    {
        public bool Equals(TimelineClosedAt other)
        {
            return _value.Equals(other?._value);
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as TimelineClosedAt);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
    }

    public sealed partial class TimelineClosedAt : IComparable, IComparable<TimelineClosedAt>
    {
        public int CompareTo([CanBeNull] object obj)
        {
            return this.CompareTo(obj as TimelineClosedAt);
        }

        public int CompareTo([CanBeNull] TimelineClosedAt other)
        {
            return _value.CompareTo(other?._value);
        }
    }
}