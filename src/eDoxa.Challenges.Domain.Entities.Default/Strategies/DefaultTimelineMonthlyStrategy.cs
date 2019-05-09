// Filename: DefaultChallengeTimelineMonthlyStrategy.cs
// Date Created: 2019-04-23
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Challenges.Domain.Entities.Abstractions;
using eDoxa.Challenges.Domain.Entities.AggregateModels.ChallengeAggregate;

namespace eDoxa.Challenges.Domain.Entities.Default.Strategies
{
    public sealed class DefaultTimelineMonthlyStrategy : ITimelineStrategy
    {
        public ITimeline Timeline => new Timeline(TimeSpan.FromDays(7), TimeSpan.FromDays(21));
    }
}