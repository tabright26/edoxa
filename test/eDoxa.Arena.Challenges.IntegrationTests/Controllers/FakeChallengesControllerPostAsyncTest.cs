// Filename: FakeChallengesControllerPostAsyncTest.cs
// Date Created: 2019-06-12
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Arena.Challenges.Api;
using eDoxa.Arena.Challenges.Api.Application.Commands;
using eDoxa.Arena.Challenges.Api.ViewModels;
using eDoxa.Arena.Challenges.Domain.Fakers;
using eDoxa.Arena.Challenges.Infrastructure;
using eDoxa.Seedwork.Application.Http;
using eDoxa.Seedwork.Security.Constants;
using eDoxa.Seedwork.Testing.TestServer;
using eDoxa.Seedwork.Testing.TestServer.Extensions;

using FluentAssertions;

using IdentityModel;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.IntegrationTests.Controllers
{
    [TestClass]
    public sealed class FakeChallengesControllerPostAsyncTest
    {
        private static readonly Claim[] Claims =
        {
            new Claim(JwtClaimTypes.Subject, Guid.NewGuid().ToString()), new Claim(JwtClaimTypes.Role, CustomRoles.Administrator)
        };

        private HttpClient _httpClient;
        private ChallengesDbContext _dbContext;
        private IMapper _mapper;

        public async Task<HttpResponseMessage> ExecuteAsync(FakeChallengesCommand command)
        {
            return await _httpClient.DefaultRequestHeaders(Claims).PostAsync("api/fake/challenges", new JsonContent(command));
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

        [DataRow(100)]
        [DataRow(1000)]
        [DataRow(10000)]
        [DataTestMethod]
        public async Task Ok(int seed)
        {
            var command = new FakeChallengesCommand(2, seed);

            var response = await this.ExecuteAsync(command);

            response.EnsureSuccessStatusCode();

            var challengeViewModels1 = await response.DeserializeAsync<ChallengeViewModel[]>();

            var challengeFaker = new ChallengeFaker();

            challengeFaker.UseSeed(seed);

            var challenges = challengeFaker.Generate(2);

            var challengeViewModel2 = _mapper.Deserialize<IEnumerable<ChallengeViewModel>>(challenges);

            challengeViewModels1.Should().BeEquivalentTo(challengeViewModel2);
        }
    }
}
