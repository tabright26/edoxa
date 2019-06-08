// Filename: ChallengeQuery.cs
// Date Created: 2019-05-20
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

using eDoxa.Arena.Challenges.Application.Abstractions.Queries;
using eDoxa.Arena.Challenges.Application.ViewModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Repositories;
using eDoxa.Security.Extensions;
using eDoxa.Seedwork.Domain.Common.Enumerations;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Http;

namespace eDoxa.Arena.Challenges.Application.Queries
{
    public sealed partial class ChallengeQuery
    {
        private readonly IMapper _mapper;
        private readonly IChallengeRepository _challengeRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ChallengeQuery(IChallengeRepository challengeRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _challengeRepository = challengeRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }
    }

    public sealed partial class ChallengeQuery : IChallengeQuery
    {
        public async Task<IReadOnlyCollection<ChallengeViewModel>> GetChallengesAsync([CanBeNull] Game game = null, [CanBeNull] ChallengeState state = null)
        {
            var challenges = await _challengeRepository.FindChallengesAsNoTrackingAsync(game, state);

            return _mapper.Map<IReadOnlyCollection<ChallengeViewModel>>(challenges);
        }

        [ItemCanBeNull]
        public async Task<ChallengeViewModel> GetChallengeAsync(ChallengeId challengeId)
        {
            var challenge = await _challengeRepository.FindChallengeAsNoTrackingAsync(challengeId);

            return _mapper.Map<ChallengeViewModel>(challenge);
        }

        public async Task<IReadOnlyCollection<ChallengeViewModel>> FindUserChallengeHistoryAsync([CanBeNull] Game game = null, [CanBeNull] ChallengeState state = null)
        {
            var userId = _httpContextAccessor.GetUserId();

            var challenges = await _challengeRepository.FindUserChallengeHistoryAsNoTrackingAsync(userId, game, state);

            return _mapper.Map<IReadOnlyCollection<ChallengeViewModel>>(challenges);
        }
    }
}
