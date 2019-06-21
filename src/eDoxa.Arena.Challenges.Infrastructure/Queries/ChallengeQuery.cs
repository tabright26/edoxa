// Filename: ChallengeQuery.cs
// Date Created: 2019-06-07
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
using eDoxa.Arena.Challenges.Infrastructure.Models;
using eDoxa.Seedwork.Common.Enumerations;
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

        public async Task<bool> ChallengeSeedExistsAsync(int seed)
        {
            return await Challenges.AnyAsync(challenge => challenge.Seed == seed);
        }

        public async Task<IReadOnlyCollection<ChallengeModel>> FindUserChallengeHistoryAsNoTrackingAsync(
            UserId userId,
            Game game = null,
            ChallengeState state = null
        )
        {
            //var challenges = from challenge in _context.Challenges.AsNoTracking().Include(NavigationPropertyPath).AsExpandable()
            //                 let participants = _context.Participants.Where(participant => participant.UserId == userId)
            //                 where challenge.Game.HasFilter(game) &&
            //                       challenge.Timeline.State.HasFilter(state) &&
            //                       participants.Any(participant => participant.Challenge.Id == challenge.Id)
            //                 select challenge;

            //return await challenges.ToListAsync();

            var challenges = await this.FindChallengesAsNoTrackingAsync(game, state);

            return challenges.Where(challenge => challenge.Participants.Any(participant => participant.UserId == userId.ToGuid())).ToList();
        }

        public async Task<IReadOnlyCollection<ChallengeModel>> FindChallengesAsNoTrackingAsync(Game game = null, ChallengeState state = null)
        {
            var challengeModels = await Challenges.Include(NavigationPropertyPath).ToListAsync();

            //challenges.Where(challenge => challenge.Game.HasFilter(game) && challenge.Timeline.State.HasFilter(state)).ToList()

            return challengeModels.ToList();
        }

        [ItemCanBeNull]
        public async Task<ChallengeModel> FindChallengeAsNoTrackingAsync(ChallengeId challengeId)
        {
            return await Challenges.Include(NavigationPropertyPath).Where(challenge => challenge.Id == challengeId.ToGuid()).SingleOrDefaultAsync();
        }
    }

    public sealed partial class ChallengeQuery : IChallengeQuery
    {
        public async Task<IReadOnlyCollection<ChallengeViewModel>> FindUserChallengeHistoryAsync(Game game = null, ChallengeState state = null)
        {
            var userId = _httpContextAccessor.GetUserId();

            var challenges = await this.FindUserChallengeHistoryAsNoTrackingAsync(userId, game, state);

            return _mapper.Map<IReadOnlyCollection<ChallengeViewModel>>(challenges);
        }

        public async Task<IReadOnlyCollection<ChallengeViewModel>> FindChallengesAsync(Game game = null, ChallengeState state = null)
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
    }
}
