// Filename: TimelineCreateAt.cs
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
    public sealed partial class TimelineCreateAt
    {
        private readonly DateTime _value;

        public TimelineCreateAt()
        {
            _value = DateTime.UtcNow;
        }

        internal TimelineCreateAt(DateTime createAt)
        {
            _value = createAt;
        }

        public static implicit operator DateTime(TimelineCreateAt createAt)
        {
            return createAt._value;
        }
    }

    public sealed partial class TimelineCreateAt : IEquatable<TimelineCreateAt>
    {
        public bool Equals(TimelineCreateAt other)
        {
            return _value.Equals(other?._value);
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as TimelineCreateAt);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
    }

    public sealed partial class TimelineCreateAt : IComparable, IComparable<TimelineCreateAt>
    {
        public int CompareTo([CanBeNull] object obj)
        {
            return this.CompareTo(obj as TimelineCreateAt);
        }

        public int CompareTo([CanBeNull] TimelineCreateAt other)
        {
            return _value.CompareTo(other?._value);
        }
    }
}