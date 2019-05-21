// Filename: TimelineCreateAt.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class TimelineCreateAt : TypeObject<TimelineCreateAt, DateTime>
    {
        public TimelineCreateAt() : base(DateTime.UtcNow)
        {
        }

        internal TimelineCreateAt(DateTime createAt) : base(createAt)
        {
        }
    }
}
