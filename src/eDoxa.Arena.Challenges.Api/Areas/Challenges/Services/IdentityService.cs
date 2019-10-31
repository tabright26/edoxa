// Filename: IdentityService.cs
// Date Created: 2019-08-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Net.Http;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Services;
using eDoxa.Seedwork.Domain.Miscs;

namespace eDoxa.Arena.Challenges.Api.Areas.Challenges.Services
{
    public sealed class IdentityService : IIdentityService
    {
        private readonly HttpClient _httpClient;

        public IdentityService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<bool> HasGameAccountIdAsync(UserId userId, Game game)
        {
            throw new NotImplementedException();
        }

        public Task<GameAccountId?> GetGameAccountIdAsync(UserId userId, Game game)
        {
            throw new NotImplementedException();
        }
    }
}
