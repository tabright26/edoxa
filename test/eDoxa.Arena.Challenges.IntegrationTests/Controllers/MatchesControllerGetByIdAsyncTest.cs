// Filename: MatchesControllerGetByIdAsyncTest.cs
// Date Created: 2019-06-13
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Arena.Challenges.Api;
using eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate;
using eDoxa.Arena.Challenges.Infrastructure;
using eDoxa.Seedwork.Security.Constants;
using eDoxa.Seedwork.Testing.TestServer;
using eDoxa.Seedwork.Testing.TestServer.Extensions;

using IdentityModel;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.IntegrationTests.Controllers
{
    [TestClass]
    public sealed class MatchesControllerGetByIdAsyncTest
    {
        private static readonly Claim[] Claims =
        {
            new Claim(JwtClaimTypes.Subject, Guid.NewGuid().ToString()), new Claim(JwtClaimTypes.Role, CustomRoles.Administrator)
        };

        private HttpClient _httpClient;
        private ChallengesDbContext _dbContext;
        private IMapper _mapper;

        public async Task<HttpResponseMessage> ExecuteAsync(MatchId matchId)
        {
            return await _httpClient.DefaultRequestHeaders(Claims).GetAsync($"api/matches/{matchId}");
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
