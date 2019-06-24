// Filename: MatchScoreResolver.cs
// Date Created: 2019-06-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;

using AutoMapper;

using eDoxa.Arena.Challenges.Api.ViewModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Infrastructure.Models;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Api.Profiles.Resolvers
{
    internal sealed class MatchScoreResolver : IValueResolver<MatchModel, MatchViewModel, decimal>
    {
        public decimal Resolve(
            [NotNull] MatchModel source,
            [NotNull] MatchViewModel destination,
            decimal destMember,
            [NotNull] ResolutionContext context
        )
        {
            return MatchScore.Resolve(source.Stats.Select(stat => context.Mapper.Map<StatViewModel>(stat).Score));
        }
    }
}
