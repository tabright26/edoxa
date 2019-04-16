// Filename: ChallengeTimeline.cs
// Date Created: 2019-03-20
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public sealed partial class ChallengeTimeline
    {
        internal static readonly DateTime MinPublishedAt = DateTime.UtcNow;
        internal static readonly DateTime MaxPublishedAt = MinPublishedAt.AddMonths(1);
        internal static readonly TimeSpan DefaultRegistrationPeriod = TimeSpan.FromHours(4);
        internal static readonly TimeSpan MinRegistrationPeriod = TimeSpan.FromHours(4);
        internal static readonly TimeSpan MaxRegistrationPeriod = TimeSpan.FromDays(7);
        internal static readonly TimeSpan DefaultExtensionPeriod = TimeSpan.FromDays(8);
        internal static readonly TimeSpan MinExtensionPeriod = TimeSpan.FromHours(8);
        internal static readonly TimeSpan MaxExtensionPeriod = TimeSpan.FromDays(28);
    }

    public sealed partial class ChallengeTimeline : ValueObject
    {
        private bool _liveMode;
        private DateTime _createdAt;
        private DateTime? _publishedAt;
        private TimeSpan? _registrationPeriod;
        private TimeSpan? _extensionPeriod;
        private DateTime? _closedAt;

        internal ChallengeTimeline(ChallengePublisherPeriodicity periodicity) : this()
        {
            _publishedAt = DateTime.UtcNow;

            switch (periodicity)
            {
                case ChallengePublisherPeriodicity.Daily:
                    _registrationPeriod = TimeSpan.FromHours(6);
                    _extensionPeriod = TimeSpan.FromHours(18);
                    break;
                case ChallengePublisherPeriodicity.Weekly:
                    _registrationPeriod = TimeSpan.FromDays(1.5);
                    _extensionPeriod = TimeSpan.FromDays(5.5);
                    break;
                case ChallengePublisherPeriodicity.Monthly:
                    _registrationPeriod = TimeSpan.FromDays(7);
                    _extensionPeriod = TimeSpan.FromDays(21);
                    break;
            }
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
            get
            {
                return _publishedAt;
            }
            private set
            {
                var publishedAt = value ?? throw new ArgumentNullException(nameof(PublishedAt));

                publishedAt = publishedAt.ToUniversalTime();

                if (publishedAt < MinPublishedAt)
                {
                    throw new ArgumentOutOfRangeException(nameof(PublishedAt));
                }

                if (publishedAt > MaxPublishedAt)
                {
                    throw new ArgumentOutOfRangeException(nameof(PublishedAt));
                }

                _publishedAt = publishedAt;
            }
        }

        public DateTime? ClosedAt
        {
            get
            {
                return _closedAt;
            }
        }

        public TimeSpan? RegistrationPeriod
        {
            get
            {
                return _registrationPeriod;
            }
            private set
            {
                var registrationPeriod = value ?? throw new ArgumentNullException(nameof(RegistrationPeriod));

                if (registrationPeriod < MinRegistrationPeriod)
                {
                    throw new ArgumentOutOfRangeException(nameof(RegistrationPeriod));
                }

                if (registrationPeriod > MaxRegistrationPeriod)
                {
                    throw new ArgumentOutOfRangeException(nameof(RegistrationPeriod));
                }

                _registrationPeriod = registrationPeriod;
            }
        }

        public TimeSpan? ExtensionPeriod
        {
            get
            {
                return _extensionPeriod;
            }
            private set
            {
                var extensionPeriod = value ?? throw new ArgumentNullException(nameof(ExtensionPeriod));

                if (extensionPeriod < MinExtensionPeriod)
                {
                    throw new ArgumentOutOfRangeException(nameof(ExtensionPeriod));
                }

                if (extensionPeriod > MaxExtensionPeriod)
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

        public bool LiveMode
        {
            get
            {
                return _liveMode;
            }
        }

        public DateTime CreatedAt
        {
            get
            {
                return _createdAt;
            }
        }

        public DateTime? StartedAt
        {
            get
            {
                return _publishedAt + _registrationPeriod;
            }
        }

        public DateTime? EndedAt
        {
            get
            {
                return _publishedAt + _registrationPeriod + _extensionPeriod;
            }
        }

        public ChallengeState State
        {
            get
            {
                if (this.IsClosed())
                {
                    return ChallengeState.Closed;
                }

                if (this.IsEnded())
                {
                    return ChallengeState.Ended;
                }

                if (this.IsInProgress())
                {
                    return ChallengeState.InProgress;
                }

                if (this.IsOpened())
                {
                    var state = ChallengeState.Opened;

                    if (_liveMode)
                    {
                        state |= ChallengeState.InProgress;
                    }

                    return state;
                }

                return this.IsConfigured() ? ChallengeState.Configured : ChallengeState.Draft;
            }
        }

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
                _registrationPeriod = DefaultRegistrationPeriod,
                _extensionPeriod = DefaultExtensionPeriod,
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
                _registrationPeriod = DefaultRegistrationPeriod,
                _extensionPeriod = DefaultExtensionPeriod,
                _publishedAt = MinPublishedAt,
                _closedAt = ClosedAt
            };
        }

        public ChallengeTimeline Close()
        {
            if (!this.IsEnded())
            {
                throw new InvalidOperationException("The challenge can not be closed because it is not ended.");
            }

            return new ChallengeTimeline
            {
                _createdAt = CreatedAt,
                _publishedAt = PublishedAt,
                _registrationPeriod = RegistrationPeriod,
                _extensionPeriod = ExtensionPeriod,
                _closedAt = DateTime.UtcNow
            };
        }

        private bool IsDraft()
        {
            return PublishedAt == null;
        }

        private bool IsConfigured()
        {
            return !this.IsDraft() && !this.IsPublish();
        }

        private bool IsPublish()
        {
            return !this.IsDraft() && PublishedAt <= DateTime.UtcNow;
        }

        private bool IsOpened()
        {
            return this.IsPublish() && !this.IsInProgress();
        }

        private bool IsInProgress()
        {
            return !this.IsDraft() && StartedAt <= DateTime.UtcNow;
        }

        private bool IsEnded()
        {
            return !this.IsDraft() && EndedAt <= DateTime.UtcNow;
        }

        private bool IsClosed()
        {
            return ClosedAt != null;
        }
    }
}