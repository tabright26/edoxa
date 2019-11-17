// Filename: IGamesEndpoint.cs
// Date Created: 2019-11-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Seedwork.Application.Dtos;
using eDoxa.Seedwork.Domain.Miscs;

using Refit;

namespace eDoxa.Challenges.Api.HttpClients
{
    public interface IGamesHttpClient
    {
        [Get("/api/challenge/games/{game}/scoring")]
        Task<ScoringDto> GetChallengeScoringAsync([AliasAs("game")] Game game);

        [Get("/api/challenge/games/{game}/matches")]
        Task<IReadOnlyCollection<MatchDto>> GetChallengeMatchesAsync(
            [AliasAs("game")] Game game,
            [AliasAs("playerId")] string playerId,
            [AliasAs("startedAt")]
            DateTime? startedAt,
            [AliasAs("closedAt")] DateTime? closedAt
        );
    }
}
