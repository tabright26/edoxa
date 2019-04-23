// Filename: ChallengeTimelineMonthly.cs
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
    public sealed class ChallengeTimelineMonthly : ChallengeTimeline
    {
        public ChallengeTimelineMonthly() : base(TimeSpan.FromDays(7), TimeSpan.FromDays(21))
        {
        }
    }
}