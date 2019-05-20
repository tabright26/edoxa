// Filename: TimelinePublishedAt.cs
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
    public sealed partial class TimelinePublishedAt
    {
        public static readonly DateTime Min = DateTime.UtcNow;
        public static readonly DateTime Max = Min.AddMonths(1);

        public static readonly TimelinePublishedAt MinValue = new TimelinePublishedAt(Min);
        public static readonly TimelinePublishedAt MaxValue = new TimelinePublishedAt(Max);

        private readonly DateTime _value;

        public TimelinePublishedAt(DateTime publishedAt, bool validate = true)
        {
            publishedAt = publishedAt.ToUniversalTime();

            if (validate)
            {
                if (publishedAt < Min ||
                    publishedAt > Max)
                {
                    throw new ArgumentException(nameof(publishedAt));
                }
            }

            _value = publishedAt;
        }

        public static implicit operator DateTime(TimelinePublishedAt publishedAt)
        {
            return publishedAt._value;
        }

        public static TimelineStartedAt operator +(TimelinePublishedAt publishedAt, TimelineRegistrationPeriod registrationPeriod)
        {
            return new TimelineStartedAt(publishedAt, registrationPeriod);
        }
    }

    public sealed partial class TimelinePublishedAt : IEquatable<TimelinePublishedAt>
    {
        public bool Equals(TimelinePublishedAt other)
        {
            return _value.Equals(other?._value);
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as TimelinePublishedAt);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
    }

    public sealed partial class TimelinePublishedAt : IComparable, IComparable<TimelinePublishedAt>
    {
        public int CompareTo([CanBeNull] object obj)
        {
            return this.CompareTo(obj as TimelinePublishedAt);
        }

        public int CompareTo([CanBeNull] TimelinePublishedAt other)
        {
            return _value.CompareTo(other?._value);
        }
    }
}