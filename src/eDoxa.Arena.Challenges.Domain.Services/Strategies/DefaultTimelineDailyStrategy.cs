// Filename: DefaultChallengeTimelineDailyStrategy.cs
// Date Created: 2019-04-23
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Arena.Challenges.Domain.Abstractions;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;

namespace eDoxa.Arena.Challenges.Domain.Services.Strategies
{
    public sealed class DefaultTimelineDailyStrategy : ITimelineStrategy
    {
        public ITimeline Timeline => new Timeline(TimeSpan.FromHours(6), TimeSpan.FromHours(18));
    }
}