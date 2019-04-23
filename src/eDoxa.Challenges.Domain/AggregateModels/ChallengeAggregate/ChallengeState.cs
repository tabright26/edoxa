// Filename: ChallengeTimelineState.cs
// Date Created: 2019-04-22
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

namespace eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class ChallengeState
    {
        private readonly string _displayName;
        private readonly ChallengeTimeline _timeline;
        private readonly long _value;

        public ChallengeState(ChallengeTimeline timeline)
        {
            _timeline = timeline;
        }

        private ChallengeState(long value, string displayName)
        {
            _value = value;
            _displayName = displayName;
        }

        public static ChallengeState None { get; } = new ChallengeState(0, nameof(None));

        public static ChallengeState Draft { get; } = new ChallengeState(1 << 0, nameof(Draft));

        public static ChallengeState Configured { get; } = new ChallengeState(1 << 1, nameof(Configured));

        public static ChallengeState Opened { get; } = new ChallengeState(1 << 2, nameof(Opened));

        public static ChallengeState InProgress { get; } = new ChallengeState(1 << 3, nameof(InProgress));

        public static ChallengeState Ended { get; } = new ChallengeState(1 << 4, nameof(Ended));

        public static ChallengeState Closed { get; } = new ChallengeState(1 << 5, nameof(Closed));

        public static ChallengeState All { get; } = new ChallengeState(~None, nameof(All));

        public ChallengeState Current
        {
            get
            {
                if (this.IsClosed())
                {
                    return Closed;
                }

                if (this.IsEnded())
                {
                    return Ended;
                }

                if (this.IsInProgress())
                {
                    return InProgress;
                }

                if (this.IsOpened())
                {
                    return Opened;
                }

                return this.IsConfigured() ? Configured : Draft;
            }
        }

        public static implicit operator long(ChallengeState state)
        {
            return state._value;
        }

        public override string ToString()
        {
            return _displayName;
        }

        private bool IsDraft()
        {
            return _timeline.PublishedAt == null;
        }

        private bool IsConfigured()
        {
            return !this.IsDraft() && !this.IsPublish();
        }

        private bool IsPublish()
        {
            return !this.IsDraft() && _timeline.PublishedAt <= DateTime.UtcNow;
        }

        private bool IsOpened()
        {
            return this.IsPublish() && !this.IsInProgress();
        }

        private bool IsInProgress()
        {
            return !this.IsDraft() && _timeline.StartedAt <= DateTime.UtcNow;
        }

        private bool IsEnded()
        {
            return !this.IsDraft() && _timeline.EndedAt <= DateTime.UtcNow;
        }

        private bool IsClosed()
        {
            return _timeline.ClosedAt != null;
        }
    }
}