// Filename: ParticipantsControllerGetByIdAsyncTest.cs
// Date Created: 2019-06-13
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Arena.Challenges.Api;
using eDoxa.Arena.Challenges.Api.ViewModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ParticipantAggregate;
using eDoxa.Arena.Challenges.Domain.Fakers;
using eDoxa.Arena.Challenges.Infrastructure;
using eDoxa.Seedwork.Security.Constants;
using eDoxa.Seedwork.Testing.TestServer;
using eDoxa.Seedwork.Testing.TestServer.Extensions;

using FluentAssertions;

using IdentityModel;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.IntegrationTests.Controllers
{
    [TestClass]
    public sealed class ParticipantsControllerGetByIdAsyncTest
    {
        private static readonly Claim[] Claims =
        {
            new Claim(JwtClaimTypes.Subject, Guid.NewGuid().ToString()), new Claim(JwtClaimTypes.Role, CustomRoles.Administrator)
        };

        private HttpClient _httpClient;
        private ChallengesDbContext _dbContext;
        private IMapper _mapper;

        public async Task<HttpResponseMessage> ExecuteAsync(ParticipantId participantId)
        {
            return await _httpClient.DefaultRequestHeaders(Claims).GetAsync($"api/participants/{participantId}");
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
        public async Task T1()
        {
            var challengeFaker = new ChallengeFaker(state: ChallengeState.Ended);

            var challenge = challengeFaker.Generate();

            _dbContext.Challenges.Add(challenge);

            await _dbContext.SaveChangesAsync();

            var participant = challenge.Participants.First();

            var response = await this.ExecuteAsync(participant.Id);

            response.EnsureSuccessStatusCode();

            var challengeViewModel1 = await response.DeserializeAsync<ParticipantViewModel>();

            var challengeViewModel2 = _mapper.Deserialize<ParticipantViewModel>(participant);

            challengeViewModel1.Should().BeEquivalentTo(challengeViewModel2);
        }
    }
}
