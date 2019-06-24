// Filename: ChallengeTimelineModelExtensions.cs
// Date Created: 2019-06-24
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Infrastructure.Models;

namespace eDoxa.Arena.Challenges.Infrastructure.Extensions
{
    public static class ChallengeTimelineModelExtensions
    {
        public static ChallengeState ResolveState(this ChallengeTimelineModel model)
        {
            return ChallengeState.From(TimeSpan.FromTicks(model.Duration), model.StartedAt, model.ClosedAt);
        }
    }
}
