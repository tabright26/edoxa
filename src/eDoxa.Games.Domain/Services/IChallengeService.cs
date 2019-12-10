// Filename: IChallengeService.cs
// Date Created: 2019-11-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Games.Domain.AggregateModels.GameAggregate;
using eDoxa.Seedwork.Domain.Dtos;
using eDoxa.Seedwork.Domain.Misc;


namespace eDoxa.Games.Domain.Services
{
    public interface IChallengeService
    {
        Task<Scoring> GetScoringAsync(Game game);

        Task<IReadOnlyCollection<ChallengeMatch>> GetMatchesAsync(
            Game game,
            PlayerId playerId,
            DateTime? startedAt,
            DateTime? endedAt
        );
    }
}
