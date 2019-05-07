// Filename: ChallengeTimelineState.cs
// Date Created: 2019-04-23
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

namespace eDoxa.Challenges.Domain.Entities.AggregateModels.ChallengeAggregate
{
    public sealed class ChallengeTimelineState
    {
        private readonly Timeline _timeline;

        public ChallengeTimelineState(Timeline timeline)
        {
            _timeline = timeline;
        }

        public ChallengeState1 Current =>
            this.IsClosed() ? ChallengeState1.Closed :
            this.IsEnded() ? ChallengeState1.Ended :
            this.IsInProgress() ? ChallengeState1.InProgress :
            this.IsOpened() ? ChallengeState1.Opened :
            this.IsConfigured() ? ChallengeState1.Configured : ChallengeState1.Draft;

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