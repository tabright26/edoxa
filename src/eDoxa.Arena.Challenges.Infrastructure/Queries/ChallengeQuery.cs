// Filename: ChallengeQuery.cs
// Date Created: 2019-06-19
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Arena.Challenges.Domain.Abstractions.Queries;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.ViewModels;
using eDoxa.Arena.Challenges.Infrastructure.Extensions;
using eDoxa.Arena.Challenges.Infrastructure.Models;
using eDoxa.Seedwork.Common.ValueObjects;
using eDoxa.Seedwork.Security.Extensions;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace eDoxa.Arena.Challenges.Infrastructure.Queries
{
    public sealed partial class ChallengeQuery
    {
        private const string NavigationPropertyPath = "Participants.Matches";

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public ChallengeQuery(ChallengesDbContext challengesDbContext, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            Challenges = challengesDbContext.Challenges.AsNoTracking();
        }

        private IQueryable<ChallengeModel> Challenges { get; }

        public async Task<IReadOnlyCollection<ChallengeModel>> FindUserChallengeHistoryAsNoTrackingAsync(
            UserId userId,
            ChallengeGame game = null,
            ChallengeState state = null
        )
        {
            var challenges = Challenges.Include(NavigationPropertyPath)
                .Where(challenge => challenge.Participants.Any(participant => participant.UserId == userId));

            challenges = Filters(challenges, game, state);

            return await challenges.ToListAsync();
        }

        public async Task<IReadOnlyCollection<ChallengeModel>> FindChallengesAsNoTrackingAsync(ChallengeGame game = null, ChallengeState state = null)
        {
            var challenges = Challenges.Include(NavigationPropertyPath);

            challenges = Filters(challenges, game, state);

            return await challenges.ToListAsync();
        }

        [ItemCanBeNull]
        public async Task<ChallengeModel> FindChallengeAsNoTrackingAsync(ChallengeId challengeId)
        {
            return await Challenges.Include(NavigationPropertyPath).Where(challenge => challenge.Id == challengeId).SingleOrDefaultAsync();
        }

        private static IQueryable<ChallengeModel> Filters(IQueryable<ChallengeModel> challenges, ChallengeGame game = null, ChallengeState state = null)
        {
            if (game != null)
            {
                challenges = challenges.Where(challenge => challenge.Game == game.Value);
            }

            if (state != null)
            {
                challenges = challenges.Where(challenge => challenge.Timeline.ResolveState() == state);
            }

            return challenges;
        }
    }

    public sealed partial class ChallengeQuery : IChallengeQuery
    {
        public async Task<IReadOnlyCollection<ChallengeViewModel>> FindUserChallengeHistoryAsync(ChallengeGame game = null, ChallengeState state = null)
        {
            var userId = _httpContextAccessor.GetUserId();

            var challenges = await this.FindUserChallengeHistoryAsNoTrackingAsync(userId, game, state);

            return _mapper.Map<IReadOnlyCollection<ChallengeViewModel>>(challenges);
        }

        public async Task<IReadOnlyCollection<ChallengeViewModel>> FindChallengesAsync(ChallengeGame game = null, ChallengeState state = null)
        {
            var challenges = await this.FindChallengesAsNoTrackingAsync(game, state);

            return _mapper.Map<IReadOnlyCollection<ChallengeViewModel>>(challenges);
        }

        [ItemCanBeNull]
        public async Task<ChallengeViewModel> FindChallengeAsync(ChallengeId challengeId)
        {
            var challenge = await this.FindChallengeAsNoTrackingAsync(challengeId);

            return _mapper.Map<ChallengeViewModel>(challenge);
        }

        public async Task<bool> ChallengeExistsAsync(ChallengeId challengeId)
        {
            return await Challenges.AnyAsync(challenge => challenge.Id == challengeId);
        }
    }
}
