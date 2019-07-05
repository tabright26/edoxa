// Filename: ParticipantScoreResolver.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;

using AutoMapper;

using eDoxa.Arena.Challenges.Domain.ViewModels;
using eDoxa.Arena.Challenges.Infrastructure.Models;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Api.Application.Profiles.Resolvers
{
    internal sealed class ParticipantScoreResolver : IValueResolver<ParticipantModel, ParticipantViewModel, decimal?>
    {
        public decimal? Resolve(
            [NotNull] ParticipantModel participantModel,
            [NotNull] ParticipantViewModel participantViewModel,
            decimal? averageScore,
            [NotNull] ResolutionContext context
        )
        {
            if (participantModel.Matches.Count >= participantModel.Challenge.Setup.BestOf)
            {
                return participantModel.Matches.Select(match => context.Mapper.Map<MatchViewModel>(match).TotalScore)
                    .OrderByDescending(totalScore => totalScore)
                    .Take(participantModel.Challenge.Setup.BestOf)
                    .Average(totalScore => totalScore);
            }

            return null;
        }
    }
}
