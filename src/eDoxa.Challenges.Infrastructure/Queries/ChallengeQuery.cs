// Filename: ChallengeQuery.cs
// Date Created: 2020-01-23
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.Queries;
using eDoxa.Challenges.Infrastructure.Extensions;
using eDoxa.Challenges.Infrastructure.Models;
using eDoxa.Seedwork.Domain.Misc;

using LinqKit;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Challenges.Infrastructure.Queries
{
    public sealed partial class ChallengeQuery
    {
        public ChallengeQuery(ChallengesDbContext context)
        {
            Challenges = context.Set<ChallengeModel>().AsNoTracking();
        }

        private IQueryable<ChallengeModel> Challenges { get; }

        private async Task<IReadOnlyCollection<ChallengeModel>> FetchUserChallengeHistoryAsync(Guid userId, int? game = null, int? state = null)
        {
            var challenges = from challenge in Challenges.Include(challenge => challenge.Participants)
                                 .ThenInclude(participant => participant.Matches)
                                 .AsExpandable()
                             where challenge.Participants.Any(participant => participant.UserId == userId) &&
                                   (game == null || challenge.Game == game) &&
                                   (state == null || challenge.State == state)
                             select challenge;

            return await challenges.ToListAsync();
        }

        private async Task<IReadOnlyCollection<ChallengeModel>> FetchChallengeModelsAsync(int? game = null, int? state = null)
        {
            var challenges = from challenge in Challenges.Include(challenge => challenge.Participants)
                                 .ThenInclude(participant => participant.Matches)
                                 .AsExpandable()
                             where (game == null || challenge.Game == game) && (state == null || challenge.State == state)
                             select challenge;

            return await challenges.ToListAsync();
        }

        private async Task<ChallengeModel?> FindChallengeModelAsync(Guid challengeId)
        {
            var challenges = from challenge in Challenges.Include(challenge => challenge.Participants)
                                 .ThenInclude(participant => participant.Matches)
                                 .AsExpandable()
                             where challenge.Id == challengeId
                             select challenge;

            return await challenges.SingleOrDefaultAsync();
        }
    }

    public sealed partial class ChallengeQuery : IChallengeQuery
    {
        public async Task<IReadOnlyCollection<IChallenge>> FetchUserChallengeHistoryAsync(UserId userId, Game? game = null, ChallengeState? state = null)
        {
            var challenges = await this.FetchUserChallengeHistoryAsync(userId, game?.Value, state?.Value);

            return challenges.Select(challenge => challenge.ToEntity()).ToList();
        }

        public async Task<IReadOnlyCollection<IChallenge>> FetchChallengesAsync(Game? game = null, ChallengeState? state = null)
        {
            var challenges = await this.FetchChallengeModelsAsync(game?.Value, state?.Value);

            return challenges.Select(challenge => challenge.ToEntity()).ToList();
        }

        public async Task<IChallenge?> FindChallengeAsync(ChallengeId challengeId)
        {
            var challenge = await this.FindChallengeModelAsync(challengeId);

            return challenge?.ToEntity();
        }
    }
}
