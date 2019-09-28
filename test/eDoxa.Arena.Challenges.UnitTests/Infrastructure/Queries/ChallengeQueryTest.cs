// Filename: ChallengeQueryTest.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Api.Infrastructure.Queries;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Infrastructure;
using eDoxa.Arena.Challenges.Infrastructure.Repositories;
using eDoxa.Arena.Challenges.UnitTests.TestHelpers;
using eDoxa.Arena.Challenges.UnitTests.TestHelpers.Assertions.Extensions;
using eDoxa.Arena.Challenges.UnitTests.TestHelpers.Extensions;
using eDoxa.Seedwork.Testing;

using IdentityModel;

using Microsoft.AspNetCore.Http;

using Moq;

using Xunit;

namespace eDoxa.Arena.Challenges.UnitTests.Infrastructure.Queries
{
    public sealed class ChallengeQueryTest : UnitTest
    {
        private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;

        public ChallengeQueryTest(ChallengeFakerFixture challengeFaker) : base(challengeFaker)
        {
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
        }

        public static IEnumerable<object[]> DataQueryParameters =>
            ChallengeGame.GetEnumerations().SelectMany(game => ChallengeState.GetEnumerations().Select(state => new object[] {game, state})).ToList();

        [Theory]
        [MemberData(nameof(DataQueryParameters))]
        public async Task FetchUserChallengeHistoryAsync_WhenChallengeQuery_ShouldBeChallenge(ChallengeGame game, ChallengeState state)
        {
            //Arrange
            var challengeFaker = ChallengeFaker.Factory.CreateFaker(84566374, game, state);

            var challenge = challengeFaker.FakeChallenge();

            _mockHttpContextAccessor.Setup(accessor => accessor.HttpContext.User.Claims)
                .Returns(new[] {new Claim(JwtClaimTypes.Subject, challenge.Participants.First().UserId.ToString())});

            using var factory = new InMemoryDbContextFactory<ArenaChallengesDbContext>();

            using (var context = factory.CreateContext())
            {
                var challengeRepository = new ChallengeRepository(context, MapperExtensions.Mapper);

                challengeRepository.Create(challenge);

                await challengeRepository.CommitAsync();
            }

            using (var context = factory.CreateContext())
            {
                var challengeQuery = new ChallengeQuery(context, MapperExtensions.Mapper, _mockHttpContextAccessor.Object);

                //Act
                var challenges = await challengeQuery.FetchUserChallengeHistoryAsync(game, state);

                //Assert
                challenges.Single().Should().Be(challenge);
            }
        }

        [Theory]
        [MemberData(nameof(DataQueryParameters))]
        public async Task FetchChallengesAsync_ShouldHaveCount(ChallengeGame game, ChallengeState state)
        {
            //Arrange
            var challengeFaker = ChallengeFaker.Factory.CreateFaker(84936374);

            var fakeChallenges = challengeFaker.FakeChallenges(4);

            using var factory = new InMemoryDbContextFactory<ArenaChallengesDbContext>();

            using (var context = factory.CreateContext())
            {
                var challengeRepository = new ChallengeRepository(context, MapperExtensions.Mapper);

                challengeRepository.Create(fakeChallenges);

                await challengeRepository.CommitAsync();
            }

            using (var context = factory.CreateContext())
            {
                var challengeQuery = new ChallengeQuery(context, MapperExtensions.Mapper, _mockHttpContextAccessor.Object);

                //Act
                var challenges = await challengeQuery.FetchChallengesAsync(game, state);

                //Assert
                challenges.Should().HaveCount(fakeChallenges.Count(challenge => challenge.Game == game && challenge.Timeline == state));
            }
        }

        [Theory]
        [MemberData(nameof(DataQueryParameters))]
        public async Task FindChallengeAsync_ShouldBeChallenge(ChallengeGame game, ChallengeState state)
        {
            //Arrange
            var challengeFaker = ChallengeFaker.Factory.CreateFaker(84568994, game, state);

            var challenge = challengeFaker.FakeChallenge();

            using var factory = new InMemoryDbContextFactory<ArenaChallengesDbContext>();

            using (var context = factory.CreateContext())
            {
                var challengeRepository = new ChallengeRepository(context, MapperExtensions.Mapper);

                challengeRepository.Create(challenge);

                await challengeRepository.CommitAsync();
            }

            using (var context = factory.CreateContext())
            {
                var challengeQuery = new ChallengeQuery(context, MapperExtensions.Mapper, _mockHttpContextAccessor.Object);

                //Act
                var challengeAsync = await challengeQuery.FindChallengeAsync(challenge.Id);

                //Assert
                challengeAsync.Should().Be(challenge);
            }
        }
    }
}
