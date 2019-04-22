// Filename: ExtensionPeriod.cs
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
    public sealed class ExtensionPeriod
    {
        internal static readonly TimeSpan DefaultExtensionPeriod = TimeSpan.FromDays(8);
        internal static readonly TimeSpan MinExtensionPeriod = TimeSpan.FromHours(8);
        internal static readonly TimeSpan MaxExtensionPeriod = TimeSpan.FromDays(28);

        private readonly TimeSpan _value;

        public ExtensionPeriod(TimeSpan extensionPeriod)
        {
            if (extensionPeriod < MinExtensionPeriod ||
                extensionPeriod > MaxExtensionPeriod /*||
                _registrationPeriod == null ||
                extensionPeriod.Ticks < _registrationPeriod?.Ticks * 3*/)
            {
                throw new ArgumentException(nameof(extensionPeriod));
            }

            _value = extensionPeriod;
        }

        public static implicit operator TimeSpan(ExtensionPeriod extensionPeriod)
        {
            return extensionPeriod._value;
        }
    }
}