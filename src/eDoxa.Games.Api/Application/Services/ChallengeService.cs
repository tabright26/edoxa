// Filename: ChallengeService.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Games.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Games.Domain.Factories;
using eDoxa.Games.Domain.Services;
using eDoxa.Grpc.Protos.Games.Dtos;
using eDoxa.Grpc.Protos.Games.Enums;
using eDoxa.Grpc.Protos.Games.Options;
using eDoxa.Seedwork.Domain.Extensions;
using eDoxa.Seedwork.Domain.Misc;

using Microsoft.Extensions.Options;

namespace eDoxa.Games.Api.Application.Services
{
    public sealed class ChallengeService : IChallengeService
    {
        private readonly IOptionsSnapshot<GamesApiOptions> _optionsSnapshot;
        private readonly IChallengeMatchesFactory _challengeMatchesFactory;

        public ChallengeService(IOptionsSnapshot<GamesApiOptions> optionsSnapshot, IChallengeMatchesFactory challengeMatchesFactory)
        {
            _optionsSnapshot = optionsSnapshot;
            _challengeMatchesFactory = challengeMatchesFactory;
        }

        private GamesApiOptions Options => _optionsSnapshot.Value;

        public async Task<ChallengeScoringDto> GetScoringAsync(Game game)
        {
            var gameOptions = await Task.FromResult(Options.Static.Games.SingleOrDefault(x => x.Name == game.ToEnum<EnumGame>()));

            return gameOptions != null ? gameOptions.Scoring : throw new InvalidOperationException($"The game ({game}) is not supported at the moment.");
        }

        public async Task<IReadOnlyCollection<ChallengeMatch>> GetMatchesAsync(
            Game game,
            PlayerId gamePlayerId,
            DateTime? startedAt,
            DateTime? endedAt,
            IImmutableSet<string> matchIds
        )
        {
            var adapter = _challengeMatchesFactory.CreateInstance(game);

            return await adapter.GetMatchesAsync(
                gamePlayerId,
                startedAt,
                endedAt,
                matchIds);
        }
    }
}
