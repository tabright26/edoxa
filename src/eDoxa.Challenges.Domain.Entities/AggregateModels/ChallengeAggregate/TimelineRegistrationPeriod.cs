// Filename: TimelineRegistrationPeriod.cs
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
    public sealed partial class TimelineRegistrationPeriod
    {
        internal static readonly TimeSpan Min = TimeSpan.FromHours(4);
        internal static readonly TimeSpan Max = TimeSpan.FromDays(7);
        internal static readonly TimeSpan Default = TimeSpan.FromHours(4);

        internal static readonly TimelineRegistrationPeriod MinValue = new TimelineRegistrationPeriod(Min);
        internal static readonly TimelineRegistrationPeriod MaxValue = new TimelineRegistrationPeriod(Max);
        internal static readonly TimelineRegistrationPeriod DefaultValue = new TimelineRegistrationPeriod(Default);

        private readonly TimeSpan _value;

        public TimelineRegistrationPeriod(TimeSpan registrationPeriod, bool validate = true)
        {
            if (validate)
            {
                if (registrationPeriod < Min ||
                    registrationPeriod > Max)
                {
                    throw new ArgumentException(nameof(registrationPeriod));
                }
            }

            _value = registrationPeriod;
        }

        public static implicit operator TimeSpan(TimelineRegistrationPeriod registrationPeriod)
        {
            return registrationPeriod._value;
        }
    }

    public sealed partial class TimelineRegistrationPeriod : IEquatable<TimelineRegistrationPeriod>
    {
        public bool Equals(TimelineRegistrationPeriod other)
        {
            return _value.Equals(other?._value);
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as TimelineRegistrationPeriod);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
    }

    public sealed partial class TimelineRegistrationPeriod : IComparable, IComparable<TimelineRegistrationPeriod>
    {
        public int CompareTo([CanBeNull] object obj)
        {
            return this.CompareTo(obj as TimelineRegistrationPeriod);
        }

        public int CompareTo([CanBeNull] TimelineRegistrationPeriod other)
        {
            return _value.CompareTo(other?._value);
        }
    }
}