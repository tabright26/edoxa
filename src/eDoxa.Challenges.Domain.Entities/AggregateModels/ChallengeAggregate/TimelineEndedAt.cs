// Filename: TimelineEndedAt.cs
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

namespace eDoxa.Challenges.Domain.Entities.AggregateModels.ChallengeAggregate
{
    public sealed partial class TimelineEndedAt
    {
        private readonly DateTime _value;

        public TimelineEndedAt(TimelineStartedAt startedAt, TimelineExtensionPeriod extensionPeriod)
        {
            _value = startedAt + extensionPeriod;
        }

        public static implicit operator DateTime(TimelineEndedAt startedAt)
        {
            return startedAt._value;
        }
    }

    public sealed partial class TimelineEndedAt : IEquatable<TimelineEndedAt>
    {
        public bool Equals(TimelineEndedAt other)
        {
            return _value.Equals(other?._value);
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as TimelineEndedAt);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
    }

    public sealed partial class TimelineEndedAt : IComparable, IComparable<TimelineEndedAt>
    {
        public int CompareTo([CanBeNull] object obj)
        {
            return this.CompareTo(obj as TimelineEndedAt);
        }

        public int CompareTo([CanBeNull] TimelineEndedAt other)
        {
            return _value.CompareTo(other?._value);
        }
    }
}