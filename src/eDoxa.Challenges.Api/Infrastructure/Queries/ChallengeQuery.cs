﻿// Filename: ChallengeQuery.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.Queries;
using eDoxa.Challenges.Infrastructure;
using eDoxa.Challenges.Infrastructure.Models;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Domain.Miscs;

using LinqKit;

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace eDoxa.Challenges.Api.Infrastructure.Queries
{
    public sealed partial class ChallengeQuery
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ChallengeQuery(ChallengesDbContext challengesDbContext, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            Mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            Challenges = challengesDbContext.Challenges.AsNoTracking();
        }

        private IQueryable<ChallengeModel> Challenges { get; }

        public IMapper Mapper { get; }

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
            var challengeModels = await this.FetchUserChallengeHistoryAsync(userId, game?.Value, state?.Value);

            return Mapper.Map<IReadOnlyCollection<IChallenge>>(challengeModels);
        }

        public async Task<IReadOnlyCollection<IChallenge>> FetchUserChallengeHistoryAsync(Game? game = null, ChallengeState? state = null)
        {
            var userId = _httpContextAccessor.GetUserId();

            return await this.FetchUserChallengeHistoryAsync(userId, game, state);
        }

        public async Task<IReadOnlyCollection<IChallenge>> FetchChallengesAsync(Game? game = null, ChallengeState? state = null)
        {
            var challengeModels = await this.FetchChallengeModelsAsync(game?.Value, state?.Value);

            return Mapper.Map<IReadOnlyCollection<IChallenge>>(challengeModels);
        }

        public async Task<IChallenge?> FindChallengeAsync(ChallengeId challengeId)
        {
            var challengeModel = await this.FindChallengeModelAsync(challengeId);

            return Mapper.Map<IChallenge>(challengeModel);
        }
    }
}