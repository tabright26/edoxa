// Filename: ChallengeStateResolver.cs
// Date Created: 2019-06-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using AutoMapper;

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.ViewModels;
using eDoxa.Arena.Challenges.Infrastructure.Models;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Api.Profiles.Resolvers
{
    internal sealed class ChallengeStateResolver : IValueResolver<ChallengeModel, ChallengeViewModel, ChallengeState>
    {
        [NotNull]
        public ChallengeState Resolve(
            [NotNull] ChallengeModel source,
            [NotNull] ChallengeViewModel destination,
            [NotNull] ChallengeState destMember,
            [NotNull] ResolutionContext context
        )
        {
            return ChallengeState.Resolve(TimeSpan.FromTicks(source.Timeline.Duration), source.Timeline.StartedAt, source.Timeline.ClosedAt);
        }
    }
}
