// Filename: MatchQueryExtensions.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Challenges.Domain.Queries;
using eDoxa.Challenges.Infrastructure.Models;
using eDoxa.Challenges.Responses;
using eDoxa.Seedwork.Domain.Miscs;

namespace eDoxa.Challenges.Api.Infrastructure.Queries.Extensions
{
    public static class MatchQueryExtensions
    {
        public static async Task<IReadOnlyCollection<MatchModel>> FetchParticipantMatchModelsAsync(this IMatchQuery challengeQuery, ParticipantId participantId)
        {
            var matches = await challengeQuery.FetchParticipantMatchesAsync(participantId);

            return challengeQuery.Mapper.Map<IReadOnlyCollection<MatchModel>>(matches);
        }

        public static async Task<MatchModel?> FindMatchModelAsync(this IMatchQuery challengeQuery, MatchId matchId)
        {
            var match = await challengeQuery.FindMatchAsync(matchId);

            return challengeQuery.Mapper.Map<MatchModel>(match);
        }

        public static async Task<IReadOnlyCollection<MatchResponse>> FetchParticipantMatchResponsesAsync(
            this IMatchQuery challengeQuery,
            ParticipantId participantId
        )
        {
            var matches = await challengeQuery.FetchParticipantMatchesAsync(participantId);

            return challengeQuery.Mapper.Map<IReadOnlyCollection<MatchResponse>>(matches);
        }

        public static async Task<MatchResponse?> FindMatchResponseAsync(this IMatchQuery challengeQuery, MatchId matchId)
        {
            var match = await challengeQuery.FindMatchAsync(matchId);

            return challengeQuery.Mapper.Map<MatchResponse>(match);
        }
    }
}
