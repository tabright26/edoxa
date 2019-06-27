// Filename: ChallengeQuery.cs
// Date Created: 2019-06-24
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Queries;
using eDoxa.Arena.Challenges.Domain.ViewModels;
using eDoxa.Arena.Challenges.Infrastructure;
using eDoxa.Arena.Challenges.Infrastructure.Models;
using eDoxa.Seedwork.Common.Extensions;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace eDoxa.Arena.Challenges.Api.Application.Queries
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

        private async Task<IReadOnlyCollection<ChallengeModel>> FindUserChallengeHistoryAsNoTrackingAsync(Guid userId, int? game = null, int? state = null)
        {
            var challenges = from challenge in Challenges.Include(NavigationPropertyPath)
                             where challenge.Participants.Any(participant => participant.UserId == userId) &&
                                   (game == null || challenge.Game == game) &&
                                   (state == null || challenge.State == state)
                             select challenge;

            return await challenges.ToListAsync();
        }

        private async Task<IReadOnlyCollection<ChallengeModel>> FindChallengesAsNoTrackingAsync(int? game = null, int? state = null)
        {
            var challenges = from challenge in Challenges.Include(NavigationPropertyPath)
                             where (game == null || challenge.Game == game) && (state == null || challenge.State == state)
                             select challenge;

            return await challenges.ToListAsync();
        }

        [ItemCanBeNull]
        private async Task<ChallengeModel> FindChallengeAsNoTrackingAsync(Guid challengeId)
        {
            var challenges = from challenge in Challenges.Include(NavigationPropertyPath)
                             where challenge.Id == challengeId
                             select challenge;

            return await challenges.SingleOrDefaultAsync();
        }
    }

    public sealed partial class ChallengeQuery : IChallengeQuery
    {
        public async Task<IReadOnlyCollection<ChallengeViewModel>> FindUserChallengeHistoryAsync(ChallengeGame game = null, ChallengeState state = null)
        {
            var userId = _httpContextAccessor.GetUserId();

            var challenges = await this.FindUserChallengeHistoryAsNoTrackingAsync(userId, game?.Value, state?.Value);

            return _mapper.Map<IReadOnlyCollection<ChallengeViewModel>>(challenges);
        }

        public async Task<IReadOnlyCollection<ChallengeViewModel>> FindChallengesAsync(ChallengeGame game = null, ChallengeState state = null)
        {
            var challenges = await this.FindChallengesAsNoTrackingAsync(game?.Value, state?.Value);

            return _mapper.Map<IReadOnlyCollection<ChallengeViewModel>>(challenges);
        }

        [ItemCanBeNull]
        public async Task<ChallengeViewModel> FindChallengeAsync(ChallengeId challengeId)
        {
            var challenge = await this.FindChallengeAsNoTrackingAsync(challengeId);

            return _mapper.Map<ChallengeViewModel>(challenge);
        }
    }
}
