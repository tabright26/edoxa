// Filename: ChallengeTimelineWeekly.cs
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
    public sealed class ChallengeTimelineWeekly : ChallengeTimeline
    {
        public ChallengeTimelineWeekly() : base(TimeSpan.FromDays(1.5), TimeSpan.FromDays(5.5))
        {
        }
    }
}