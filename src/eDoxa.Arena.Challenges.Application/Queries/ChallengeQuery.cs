// Filename: ChallengeQuery.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Repositories;
using eDoxa.Arena.Challenges.DTO;
using eDoxa.Arena.Challenges.DTO.Queries;
using eDoxa.Functional;
using eDoxa.Security.Extensions;
using eDoxa.Seedwork.Domain.Enumerations;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Http;

namespace eDoxa.Arena.Challenges.Application.Queries
{
    internal sealed partial class ChallengeQuery
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

    internal sealed partial class ChallengeQuery : IChallengeQuery
    {
        public async Task<Option<ChallengeListDTO>> GetChallengesAsync([CanBeNull] Game game = null, [CanBeNull] ChallengeState state = null)
        {
            var challenges = await _challengeRepository.FindChallengesAsNoTrackingAsync(game, state);

            var list = _mapper.Map<ChallengeListDTO>(challenges);

            return list.Any() ? new Option<ChallengeListDTO>(list) : new Option<ChallengeListDTO>();
        }

        public async Task<Option<ChallengeDTO>> GetChallengeAsync(ChallengeId challengeId)
        {
            var challenge = await _challengeRepository.FindChallengeAsNoTrackingAsync(challengeId);

            var mapper = _mapper.Map<ChallengeDTO>(challenge);

            return mapper != null ? new Option<ChallengeDTO>(mapper) : new Option<ChallengeDTO>();
        }

        public async Task<Option<ChallengeListDTO>> FindUserChallengeHistoryAsync([CanBeNull] Game game = null, [CanBeNull] ChallengeState state = null)
        {
            var userId = _httpContextAccessor.GetUserId();

            var challenges = await _challengeRepository.FindUserChallengeHistoryAsNoTrackingAsync(userId, game, state);

            var list = _mapper.Map<ChallengeListDTO>(challenges);

            return list.Any() ? new Option<ChallengeListDTO>(list) : new Option<ChallengeListDTO>();
        }
    }
}
