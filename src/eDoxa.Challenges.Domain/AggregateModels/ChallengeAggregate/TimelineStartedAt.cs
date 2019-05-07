// Filename: TimelineStartedAt.cs
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
    public sealed partial class TimelineStartedAt
    {
        private readonly DateTime _value;

        public TimelineStartedAt(TimelinePublishedAt publishedAt, TimelineRegistrationPeriod registrationPeriod)
        {
            _value = publishedAt + registrationPeriod;
        }

        public static implicit operator DateTime(TimelineStartedAt startedAt)
        {
            return startedAt._value;
        }

        public static TimelineEndedAt operator +(TimelineStartedAt startedAt, TimelineExtensionPeriod extensionPeriod)
        {
            return new TimelineEndedAt(startedAt, extensionPeriod);
        }
    }

    public sealed partial class TimelineStartedAt : IEquatable<TimelineStartedAt>
    {
        public bool Equals(TimelineStartedAt other)
        {
            return _value.Equals(other?._value);
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as TimelineStartedAt);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
    }

    public sealed partial class TimelineStartedAt : IComparable, IComparable<TimelineStartedAt>
    {
        public int CompareTo([CanBeNull] object obj)
        {
            return this.CompareTo(obj as TimelineStartedAt);
        }

        public int CompareTo([CanBeNull] TimelineStartedAt other)
        {
            return _value.CompareTo(other?._value);
        }
    }
}