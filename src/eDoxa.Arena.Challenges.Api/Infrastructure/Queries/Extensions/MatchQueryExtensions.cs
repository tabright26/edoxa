﻿// Filename: MatchQueryExtensions.cs
// Date Created: 2019-07-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Api.ViewModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Queries;
using eDoxa.Arena.Challenges.Infrastructure.Models;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Api.Infrastructure.Queries.Extensions
{
    public static class MatchQueryExtensions
    {
        public static async Task<IReadOnlyCollection<MatchModel>> FetchParticipantMatchModelsAsync(this IMatchQuery challengeQuery, ParticipantId participantId)
        {
            var matches = await challengeQuery.FetchParticipantMatchesAsync(participantId);

            return challengeQuery.Mapper.Map<IReadOnlyCollection<MatchModel>>(matches);
        }

        [ItemCanBeNull]
        public static async Task<MatchModel> FindMatchModelAsync(this IMatchQuery challengeQuery, MatchId matchId)
        {
            var match = await challengeQuery.FindMatchAsync(matchId);

            return challengeQuery.Mapper.Map<MatchModel>(match);
        }

        public static async Task<IReadOnlyCollection<MatchViewModel>> FetchParticipantMatchViewModelsAsync(
            this IMatchQuery challengeQuery,
            ParticipantId participantId
        )
        {
            var matches = await challengeQuery.FetchParticipantMatchesAsync(participantId);

            return challengeQuery.Mapper.Map<IReadOnlyCollection<MatchViewModel>>(matches);
        }

        [ItemCanBeNull]
        public static async Task<MatchViewModel> FindMatchViewModelAsync(this IMatchQuery challengeQuery, MatchId matchId)
        {
            var match = await challengeQuery.FindMatchAsync(matchId);

            return challengeQuery.Mapper.Map<MatchViewModel>(match);
        }
    }
}
