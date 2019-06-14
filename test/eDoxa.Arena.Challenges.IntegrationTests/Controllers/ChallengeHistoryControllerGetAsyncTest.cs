// Filename: ChallengeHistoryControllerGetAsyncTest.cs
// Date Created: 2019-06-13
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Arena.Challenges.Api;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Infrastructure;
using eDoxa.Seedwork.Common.Enumerations;
using eDoxa.Seedwork.Common.ValueObjects;
using eDoxa.Seedwork.Testing.TestServer;
using eDoxa.Seedwork.Testing.TestServer.Extensions;

using IdentityModel;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.IntegrationTests.Controllers
{
    [TestClass]
    public sealed class ChallengeHistoryControllerGetAsyncTest
    {
        private HttpClient _httpClient;
        private ChallengesDbContext _dbContext;
        private IMapper _mapper;

        public async Task<HttpResponseMessage> ExecuteAsync(UserId userId, Game game = null, ChallengeState state = null)
        {
            return await _httpClient.DefaultRequestHeaders(new[] {new Claim(JwtClaimTypes.Subject, userId.ToString())})
                .GetAsync($"api/challenges/history?game={game}&state={state}");
        }

        [TestInitialize]
        public async Task TestInitialize()
        {
            var factory = new CustomWebApplicationFactory<ChallengesDbContext, Startup>();

            _httpClient = factory.CreateClient();

            _dbContext = factory.DbContext;

            _mapper = factory.Mapper;

            await this.TestCleanup();
        }

        [TestCleanup]
        public async Task TestCleanup()
        {
            _dbContext.Challenges.RemoveRange(_dbContext.Challenges);

            await _dbContext.SaveChangesAsync();
        }
    }
}
