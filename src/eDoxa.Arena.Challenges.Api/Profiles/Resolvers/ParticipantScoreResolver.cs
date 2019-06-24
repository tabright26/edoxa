// Filename: ParticipantScoreResolver.cs
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
    internal sealed class ParticipantScoreResolver : IValueResolver<ParticipantModel, ParticipantViewModel, decimal?>
    {
        public decimal? Resolve(
            [NotNull] ParticipantModel source,
            [NotNull] ParticipantViewModel destination,
            decimal? destMember,
            [NotNull] ResolutionContext context
        )
        {
            return Participant.HasAverageScore(source.Matches.Count, source.Challenge.Setup.BestOf)
                ? ParticipantScore.Resolve(source.Matches.Select(match => context.Mapper.Map<MatchViewModel>(match).TotalScore), source.Challenge.Setup.BestOf)
                : (decimal?) null;
        }
    }
}
