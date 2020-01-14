// Filename: IChallengeService.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;

using eDoxa.Games.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Grpc.Protos.Games.Dtos;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Games.Domain.Services
{
    public interface IChallengeService
    {
        Task<ChallengeScoringDto> GetScoringAsync(Game game);

        Task<IReadOnlyCollection<ChallengeMatch>> GetMatchesAsync(
            Game game,
            PlayerId gamePlayerId,
            DateTime? startedAt,
            DateTime? endedAt,
            IImmutableSet<string> matchIds
        );
    }
}
