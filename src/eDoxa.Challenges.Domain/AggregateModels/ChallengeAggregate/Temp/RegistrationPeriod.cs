// Filename: RegistrationPeriod.cs
// Date Created: 2019-04-22
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

namespace eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate.Temp
{
    public sealed class RegistrationPeriod
    {
        internal static readonly TimeSpan DefaultRegistrationPeriod = TimeSpan.FromHours(4);
        internal static readonly TimeSpan MinRegistrationPeriod = TimeSpan.FromHours(4);
        internal static readonly TimeSpan MaxRegistrationPeriod = TimeSpan.FromDays(7);

        private readonly TimeSpan _value;

        public RegistrationPeriod(TimeSpan registrationPeriod)
        {
            if (registrationPeriod < MinRegistrationPeriod ||
                registrationPeriod > MaxRegistrationPeriod)
            {
                throw new ArgumentException(nameof(registrationPeriod));
            }

            _value = registrationPeriod;
        }

        public static implicit operator TimeSpan(RegistrationPeriod registrationPeriod)
        {
            return registrationPeriod._value;
        }
    }
}