﻿// Filename: TimelineExtensionPeriod.cs
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
    public sealed partial class TimelineExtensionPeriod
    {
        public static readonly TimeSpan Min = TimeSpan.FromHours(8);
        public static readonly TimeSpan Max = TimeSpan.FromDays(28);
        public static readonly TimeSpan Default = TimeSpan.FromDays(8);

        public static readonly TimelineExtensionPeriod MinValue = new TimelineExtensionPeriod(Min);
        public static readonly TimelineExtensionPeriod MaxValue = new TimelineExtensionPeriod(Max);
        public static readonly TimelineExtensionPeriod DefaultValue = new TimelineExtensionPeriod(Default);

        private readonly TimeSpan _value;

        public TimelineExtensionPeriod(TimeSpan extensionPeriod, bool validate = true)
        {
            if (validate)
            {
                if (extensionPeriod < Min ||
                    extensionPeriod > Max /*||
                _registrationPeriod == null ||
                extensionPeriod.Ticks < _registrationPeriod?.Ticks * 3*/)
                {
                    throw new ArgumentException(nameof(extensionPeriod));
                }
            }

            _value = extensionPeriod;
        }

        public static implicit operator TimeSpan(TimelineExtensionPeriod extensionPeriod)
        {
            return extensionPeriod._value;
        }
    }

    public sealed partial class TimelineExtensionPeriod : IEquatable<TimelineExtensionPeriod>
    {
        public bool Equals(TimelineExtensionPeriod other)
        {
            return _value.Equals(other?._value);
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as TimelineExtensionPeriod);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
    }

    public sealed partial class TimelineExtensionPeriod : IComparable, IComparable<TimelineExtensionPeriod>
    {
        public int CompareTo([CanBeNull] object obj)
        {
            return this.CompareTo(obj as TimelineExtensionPeriod);
        }

        public int CompareTo([CanBeNull] TimelineExtensionPeriod other)
        {
            return _value.CompareTo(other?._value);
        }
    }
}