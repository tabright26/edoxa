// Filename: PublishedAt.cs
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
    public sealed class PublishedAt
    {
        internal static readonly DateTime MinPublishedAt = DateTime.UtcNow;
        internal static readonly DateTime MaxPublishedAt = MinPublishedAt.AddMonths(1);

        private readonly DateTime _value;

        public PublishedAt(DateTime publishedAt)
        {
            publishedAt = publishedAt.ToUniversalTime();

            if (publishedAt < MinPublishedAt ||
                publishedAt > MaxPublishedAt)
            {
                throw new ArgumentException(nameof(publishedAt));
            }

            _value = publishedAt;
        }

        public static implicit operator DateTime(PublishedAt publishedAt)
        {
            return publishedAt._value;
        }

        public static StartedAt operator +(PublishedAt publishedAt, RegistrationPeriod registrationPeriod)
        {
            return new StartedAt(publishedAt, registrationPeriod);
        }
    }
}