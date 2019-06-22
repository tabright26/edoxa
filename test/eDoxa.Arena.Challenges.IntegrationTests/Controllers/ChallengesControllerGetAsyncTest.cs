// Filename: ChallengesControllerGetAsyncTest.cs
// Date Created: 2019-06-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Arena.Challenges.Api;
using eDoxa.Arena.Challenges.Domain.Fakers;
using eDoxa.Arena.Challenges.Domain.ViewModels;
using eDoxa.Arena.Challenges.Infrastructure;
using eDoxa.Arena.Challenges.Infrastructure.Extensions;
using eDoxa.Arena.Challenges.Infrastructure.Models;
using eDoxa.Seedwork.Testing.TestServer;
using eDoxa.Seedwork.Testing.TestServer.Extensions;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newtonsoft.Json;

namespace eDoxa.Arena.Challenges.IntegrationTests.Controllers
{
    [TestClass]
    public sealed class ChallengesControllerGetAsyncTest
    {
        private HttpClient _httpClient;
        private ChallengesDbContext _dbContext;
        private IMapper _mapper;

        public async Task<HttpResponseMessage> ExecuteAsync()
        {
            return await _httpClient.GetAsync("api/challenges");
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

        [TestMethod]
        public async Task When_database_empty_status_code_should_be_no_content()
        {
            var response = await this.ExecuteAsync();

            response.EnsureSuccessStatusCode();

            response.StatusCode.Should().Be(HttpStatusCode.NoContent);

            var challenges = await response.DeserializeAsync<ChallengeViewModel[]>();

            challenges.Should().BeNull();
        }

        [DataRow(2)]
        [DataRow(5)]
        [DataTestMethod]
        public async Task The_response_http_should_have_exactly_the_same_number_of_fake_challenges_added_to_the_database(int count)
        {
            var challengeFaker = new ChallengeFaker();

            var challenges = challengeFaker.GenerateModels(count);

            _dbContext.Challenges.AddRange(challenges);

            await _dbContext.SaveChangesAsync();

            var response = await this.ExecuteAsync();

            response.EnsureSuccessStatusCode();

            var challengeViewModels = await response.DeserializeAsync<ChallengeViewModel[]>();

            challengeViewModels.Should().HaveCount(count);
        }

        [DataRow(100)]
        [DataRow(1000)]
        [DataRow(10000)]
        [DataTestMethod]
        public async Task Fake_challenge_with_same_seed_should_be_equivalent(int seed)
        {
            var challenge1 = FakeChallenge(seed);

            _dbContext.Challenges.Add(challenge1);

            await _dbContext.SaveChangesAsync();

            var response = await this.ExecuteAsync();

            response.EnsureSuccessStatusCode();

            var challengeViewModels = await response.DeserializeAsync<ChallengeViewModel[]>();

            var challengeViewModel1 = challengeViewModels.First();

            var challenge2 = FakeChallenge(seed);

            var challengeViewModel2 = JsonConvert.DeserializeObject<ChallengeViewModel>(JsonConvert.SerializeObject(_mapper.Map<ChallengeViewModel>(challenge2)));

            challengeViewModel1.Should().BeEquivalentTo(challengeViewModel2);
        }

        private static ChallengeModel FakeChallenge(int seed)
        {
            var challengeFaker = new ChallengeFaker();

            challengeFaker.UseSeed(seed);

            return challengeFaker.GenerateModel();
        }
    }
}
