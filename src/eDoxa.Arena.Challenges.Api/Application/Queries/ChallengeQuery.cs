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
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Arena.Challenges.Api.Application.Abstractions;
using eDoxa.Arena.Challenges.Api.ViewModels;
using eDoxa.Arena.Challenges.Domain.Abstractions.Repositories;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Common.Enumerations;
using eDoxa.Seedwork.Security.Extensions;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Http;

namespace eDoxa.Arena.Challenges.Api.Application.Queries
{
    public sealed partial class ChallengeQuery
    {
        private readonly IChallengeRepository _challengeRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public ChallengeQuery(IChallengeRepository challengeRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _challengeRepository = challengeRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }
    }

    public sealed partial class ChallengeQuery : IChallengeQuery
    {
        public async Task<IReadOnlyCollection<ChallengeViewModel>> FindUserChallengeHistoryAsync(Game game = null, ChallengeState state = null)
        {
            var userId = _httpContextAccessor.GetUserId();

            var challenges = await _challengeRepository.FindUserChallengeHistoryAsNoTrackingAsync(userId, game, state);

            return _mapper.Map<IReadOnlyCollection<ChallengeViewModel>>(challenges);
        }

        public async Task<IReadOnlyCollection<ChallengeViewModel>> FindChallengesAsync(Game game = null, ChallengeState state = null)
        {
            var challenges = await _challengeRepository.FindChallengesAsNoTrackingAsync(game, state);

            return _mapper.Map<IReadOnlyCollection<ChallengeViewModel>>(challenges);
        }

        [ItemCanBeNull]
        public async Task<ChallengeViewModel> FindChallengeAsync(ChallengeId challengeId)
        {
            var challenge = await _challengeRepository.FindChallengeAsNoTrackingAsync(challengeId);

            return _mapper.Map<ChallengeViewModel>(challenge);
        }
    }
}
