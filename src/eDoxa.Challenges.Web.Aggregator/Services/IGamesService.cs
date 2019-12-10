// Filename: IGamesService.cs
// Date Created: 2019-11-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Seedwork.Domain.Dtos;
using eDoxa.Seedwork.Domain.Misc;

using Refit;

namespace eDoxa.Challenges.Web.Aggregator.Services
{
    public interface IGamesService
    {
        [Get("/api/challenge/games/{game}/scoring")]
        Task<ScoringDto> GetChallengeScoringAsync([AliasAs("game")] Game game);

        [Get("/api/challenge/games/{game}/matches")]
        Task<IReadOnlyCollection<ChallengeMatch>> GetChallengeMatchesAsync(
            [AliasAs("game")] Game game,
            [AliasAs("playerId")]
            string playerId,
            [AliasAs("startedAt")]
            DateTime? startedAt,
            [AliasAs("closedAt")]
            DateTime? closedAt
        );
    }
}
