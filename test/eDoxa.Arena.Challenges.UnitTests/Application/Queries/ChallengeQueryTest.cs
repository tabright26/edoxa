// Filename: ChallengeQueryTest.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Api.Application.Fakers;
using eDoxa.Arena.Challenges.Api.Infrastructure.Queries;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Infrastructure;
using eDoxa.Arena.Challenges.Infrastructure.Repositories;
using eDoxa.Arena.Challenges.UnitTests.Helpers.Extensions;
using eDoxa.Seedwork.Infrastructure.Factories;

using FluentAssertions;

using IdentityModel;

using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Arena.Challenges.UnitTests.Application.Queries
{
    [TestClass]
    public sealed class ChallengeQueryTest
    {
        private Mock<IHttpContextAccessor> _mockHttpContextAccessor;

        private static IEnumerable<object[]> DataQueryParameters =>
            ChallengeGame.GetEnumerations().SelectMany(game => ChallengeState.GetEnumerations().Select(state => new object[] {game, state})).ToList();

        [TestInitialize]
        public void TestInitialize()
        {
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
        }

        [DataTestMethod]
        [DynamicData(nameof(DataQueryParameters))]
        public async Task FetchUserChallengeHistoryAsync(ChallengeGame game, ChallengeState state)
        {
            var challengeFaker = new ChallengeFaker(game, state);

            challengeFaker.UseSeed(84566374);

            var challenge = challengeFaker.Generate();

            _mockHttpContextAccessor.Setup(accessor => accessor.HttpContext.User.Claims)
                .Returns(new[] {new Claim(JwtClaimTypes.Subject, challenge.Participants.First().UserId.ToString())});

            using (var factory = new InMemoryDbContextFactory<ArenaChallengesDbContext>())
            {
                using (var context = factory.CreateContext())
                {
                    var challengeRepository = new ChallengeRepository(context, MapperExtensions.Mapper);

                    challengeRepository.Create(challenge);

                    await challengeRepository.CommitAsync();
                }

                using (var context = factory.CreateContext())
                {
                    var challengeQuery = new ChallengeQuery(context, MapperExtensions.Mapper, _mockHttpContextAccessor.Object);

                    var challengeViewModels = await challengeQuery.FetchUserChallengeHistoryAsync(game, state);

                    challengeViewModels.Single().Should().Be(challenge);
                }
            }
        }

        [DataTestMethod]
        [DynamicData(nameof(DataQueryParameters))]
        public async Task FetchChallengesAsync(ChallengeGame game, ChallengeState state)
        {
            var challengeFaker = new ChallengeFaker();

            challengeFaker.UseSeed(84936374);

            var fakeChallenges = challengeFaker.Generate(4);

            using (var factory = new InMemoryDbContextFactory<ArenaChallengesDbContext>())
            {
                using (var context = factory.CreateContext())
                {
                    var challengeRepository = new ChallengeRepository(context, MapperExtensions.Mapper);

                    challengeRepository.Create(fakeChallenges);

                    await challengeRepository.CommitAsync();
                }

                using (var context = factory.CreateContext())
                {
                    var challengeQuery = new ChallengeQuery(context, MapperExtensions.Mapper, _mockHttpContextAccessor.Object);

                    var challenges = await challengeQuery.FetchChallengesAsync(game, state);
                    
                    challenges.Should().HaveCount(fakeChallenges.Count(challenge => challenge.Game == game && challenge.Timeline == state));
                }
            }
        }

        [DataTestMethod]
        [DynamicData(nameof(DataQueryParameters))]
        public async Task FindChallengeAsync(ChallengeGame game, ChallengeState state)
        {
            var challengeFaker = new ChallengeFaker(game, state);

            challengeFaker.UseSeed(84568994);

            var challenge = challengeFaker.Generate();

            using (var factory = new InMemoryDbContextFactory<ArenaChallengesDbContext>())
            {
                using (var context = factory.CreateContext())
                {
                    var challengeRepository = new ChallengeRepository(context, MapperExtensions.Mapper);

                    challengeRepository.Create(challenge);

                    await challengeRepository.CommitAsync();
                }

                using (var context = factory.CreateContext())
                {
                    var challengeQuery = new ChallengeQuery(context, MapperExtensions.Mapper, _mockHttpContextAccessor.Object);

                    var challengeViewModel = await challengeQuery.FindChallengeAsync(challenge.Id);

                    challengeViewModel.Should().Be(challenge);
                }
            }
        }
    }
}
