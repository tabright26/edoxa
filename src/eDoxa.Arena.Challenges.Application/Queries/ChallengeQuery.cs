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
using eDoxa.Arena.Challenges.Domain.Repositories;
using eDoxa.Arena.Challenges.DTO;
using eDoxa.Arena.Challenges.DTO.Queries;
using eDoxa.Functional;
using eDoxa.Security.Extensions;
using eDoxa.Seedwork.Domain.Enumerations;

using Microsoft.AspNetCore.Http;

namespace eDoxa.Arena.Challenges.Application.Queries
{
    internal sealed partial class ChallengeQuery
    {
        private readonly IMapper _mapper;
        private readonly IChallengeRepository _repository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ChallengeQuery(IChallengeRepository repository, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }
    }

    internal sealed partial class ChallengeQuery : IChallengeQuery
    {
        public async Task<Option<ChallengeListDTO>> FindChallengesAsync(Game game)
        {
            var challenges = await _repository.FindChallengesAsNoTrackingAsync(game);

            var list = _mapper.Map<ChallengeListDTO>(challenges);

            return list.Any() ? new Option<ChallengeListDTO>(list) : new Option<ChallengeListDTO>();
        }

        public async Task<Option<ChallengeDTO>> FindChallengeAsync(ChallengeId challengeId)
        {
            var challenge = await _repository.FindChallengeAsNoTrackingAsync(challengeId);

            var mapper = _mapper.Map<ChallengeDTO>(challenge);

            return mapper != null ? new Option<ChallengeDTO>(mapper) : new Option<ChallengeDTO>();
        }

        public async Task<Option<ChallengeListDTO>> FindUserChallengeHistoryAsync(Game game)
        {
            var userId = _httpContextAccessor.GetUserId();

            var challenges = await _repository.FindUserChallengeHistoryAsNoTrackingAsync(userId, game);

            var list = _mapper.Map<ChallengeListDTO>(challenges);

            return list.Any() ? new Option<ChallengeListDTO>(list) : new Option<ChallengeListDTO>();
        }
    }
}
