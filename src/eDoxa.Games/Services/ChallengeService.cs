using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Games.Abstractions.Factories;
using eDoxa.Games.Abstractions.Services;
using eDoxa.Seedwork.Application.Dtos;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Games.Services
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

        public async Task<ScoringDto> GetScoringAsync(Game game)
        {
            var adapter = _challengeScoringFactory.CreateInstance(game);

            return await adapter.GetScoringAsync();
        }

        public async Task<IReadOnlyCollection<MatchDto>> GetMatchesAsync(Game game, PlayerId playerId, DateTime? startedAt, DateTime? endedAt)
        {
            var adapter = _challengeMatchesFactory.CreateInstance(game);

            return await adapter.GetMatchesAsync(playerId, startedAt, endedAt);
        }
    }
}
