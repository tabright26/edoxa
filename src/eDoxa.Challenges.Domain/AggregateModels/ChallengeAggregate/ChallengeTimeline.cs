// Filename: ChallengeTimeline.cs
// Date Created: 2019-04-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public class ChallengeTimeline : ValueObject, IChallengeTimeline
    {
        private DateTime? _closedAt;
        private DateTime _createdAt;
        private TimeSpan? _extensionPeriod;
        private bool _liveMode;
        private DateTime? _publishedAt;
        private TimeSpan? _registrationPeriod;

        internal ChallengeTimeline(TimeSpan registrationPeriod, TimeSpan extensionPeriod) : this()
        {
            _registrationPeriod = registrationPeriod;
            _extensionPeriod = extensionPeriod;
            _publishedAt = DateTime.UtcNow;
        }

        internal ChallengeTimeline()
        {
            _liveMode = true;
            _createdAt = DateTime.UtcNow;
            _publishedAt = null;
            _registrationPeriod = null;
            _extensionPeriod = null;
            _closedAt = null;
        }

        public DateTime? PublishedAt
        {
            get => _publishedAt;
            private set
            {
                var publishedAt = value ?? throw new ArgumentNullException(nameof(PublishedAt));

                publishedAt = publishedAt.ToUniversalTime();

                if (publishedAt < TimelinePublishedAt.Min)
                {
                    throw new ArgumentOutOfRangeException(nameof(PublishedAt));
                }

                if (publishedAt > TimelinePublishedAt.Max)
                {
                    throw new ArgumentOutOfRangeException(nameof(PublishedAt));
                }

                _publishedAt = publishedAt;
            }
        }

        public DateTime? ClosedAt => _closedAt;

        public TimeSpan? RegistrationPeriod
        {
            get => _registrationPeriod;
            private set
            {
                var registrationPeriod = value ?? throw new ArgumentNullException(nameof(RegistrationPeriod));

                if (registrationPeriod < TimelineRegistrationPeriod.Min)
                {
                    throw new ArgumentOutOfRangeException(nameof(RegistrationPeriod));
                }

                if (registrationPeriod > TimelineRegistrationPeriod.Max)
                {
                    throw new ArgumentOutOfRangeException(nameof(RegistrationPeriod));
                }

                _registrationPeriod = registrationPeriod;
            }
        }

        public TimeSpan? ExtensionPeriod
        {
            get => _extensionPeriod;
            private set
            {
                var extensionPeriod = value ?? throw new ArgumentNullException(nameof(ExtensionPeriod));

                if (extensionPeriod < TimelineExtensionPeriod.Min)
                {
                    throw new ArgumentOutOfRangeException(nameof(ExtensionPeriod));
                }

                if (extensionPeriod > TimelineExtensionPeriod.Max)
                {
                    throw new ArgumentOutOfRangeException(nameof(ExtensionPeriod));
                }

                if (_registrationPeriod == null)
                {
                    throw new InvalidOperationException(nameof(ExtensionPeriod));
                }

                if (extensionPeriod.Ticks < _registrationPeriod?.Ticks * 3)
                {
                    throw new ArgumentOutOfRangeException(nameof(ExtensionPeriod));
                }

                _extensionPeriod = extensionPeriod;
            }
        }

        public bool LiveMode => _liveMode;

        public DateTime CreatedAt => _createdAt;

        public DateTime? StartedAt => _publishedAt + _registrationPeriod;

        public DateTime? EndedAt => _publishedAt + _registrationPeriod + _extensionPeriod;

        public ChallengeState1 State => new ChallengeTimelineState(this).Current;

        public ChallengeTimeline Configure(DateTime publishedAt, TimeSpan registrationPeriod, TimeSpan extensionPeriod)
        {
            var timeline = this.Configure(publishedAt);

            timeline.RegistrationPeriod = registrationPeriod;

            timeline.ExtensionPeriod = extensionPeriod;

            return timeline;
        }

        public ChallengeTimeline Configure(DateTime publishedAt)
        {
            return new ChallengeTimeline
            {
                _createdAt = CreatedAt,
                _registrationPeriod = TimelineRegistrationPeriod.Default,
                _extensionPeriod = TimelineExtensionPeriod.Default,
                PublishedAt = publishedAt,
                _closedAt = ClosedAt
            };
        }

        public ChallengeTimeline Publish(TimeSpan registrationPeriod, TimeSpan extensionPeriod)
        {
            var timeline = this.Publish();

            timeline.RegistrationPeriod = registrationPeriod;

            timeline.ExtensionPeriod = extensionPeriod;

            return timeline;
        }

        public ChallengeTimeline Publish()
        {
            return new ChallengeTimeline
            {
                _createdAt = CreatedAt,
                _registrationPeriod = TimelineRegistrationPeriod.Default,
                _extensionPeriod = TimelineExtensionPeriod.Default,
                _publishedAt = TimelinePublishedAt.Min,
                _closedAt = ClosedAt
            };
        }

        public ChallengeTimeline Close()
        {
            return new ChallengeTimeline
            {
                _createdAt = CreatedAt,
                _publishedAt = PublishedAt,
                _registrationPeriod = RegistrationPeriod,
                _extensionPeriod = ExtensionPeriod,
                _closedAt = DateTime.UtcNow
            };
        }
    }
}