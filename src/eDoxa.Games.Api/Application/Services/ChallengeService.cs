using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Games.Domain.AggregateModels.GameAggregate;
using eDoxa.Games.Domain.Factories;
using eDoxa.Games.Domain.Services;
using eDoxa.Seedwork.Domain.Dtos;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Games.Api.Application.Services
{
    public sealed class ChallengeService : IChallengeService
    {
        private readonly IChallengeScoringFactory _challengeScoringFactory;
        private readonly IChallengeMatchesFactory _challengeMatchesFactory;

        public ChallengeService(IChallengeScoringFactory challengeScoringFactory, IChallengeMatchesFactory challengeMatchesFactory)
        {
            _challengeScoringFactory = challengeScoringFactory;
            _challengeMatchesFactory = challengeMatchesFactory;
        }

        public async Task<Scoring> GetScoringAsync(Game game)
        {
            var adapter = _challengeScoringFactory.CreateInstance(game);

            return await adapter.GetScoringAsync();
        }

        public async Task<IReadOnlyCollection<ChallengeMatch>> GetMatchesAsync(Game game, PlayerId playerId, DateTime? startedAt, DateTime? endedAt)
        {
            var adapter = _challengeMatchesFactory.CreateInstance(game);

            return await adapter.GetMatchesAsync(playerId, startedAt, endedAt);
        }
    }
}
