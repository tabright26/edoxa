// Filename: StartedAt.cs
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
    public sealed class StartedAt
    {
        private readonly DateTime _value;

        public StartedAt(PublishedAt publishedAt, RegistrationPeriod registrationPeriod)
        {
            _value = publishedAt + registrationPeriod;
        }

        public static implicit operator DateTime(StartedAt startedAt)
        {
            return startedAt._value;
        }

        public static EndedAt operator +(StartedAt startedAt, ExtensionPeriod extensionPeriod)
        {
            return new EndedAt(startedAt, extensionPeriod);
        }
    }
}