// Filename: IdentityService.cs
// Date Created: 2019-07-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Net.Http;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Services;

namespace eDoxa.Arena.Challenges.Api.Application.Services
{
    public sealed class IdentityService : IIdentityService
    {
        private readonly HttpClient _httpClient;

        public IdentityService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<bool> HasGameAccountIdAsync(UserId userId, ChallengeGame game)
        {
            throw new NotImplementedException();
        }

        public Task<GameAccountId?> GetGameAccountIdAsync(UserId userId, ChallengeGame game)
        {
            throw new NotImplementedException();
        }
    }
}
