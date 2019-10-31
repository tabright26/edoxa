// Filename: ChallengeQueryTest.cs
// Date Created: 2019-09-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Api.Infrastructure.Queries;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Infrastructure;
using eDoxa.Arena.Challenges.Infrastructure.Repositories;
using eDoxa.Arena.Challenges.TestHelpers;
using eDoxa.Arena.Challenges.TestHelpers.Assertions.Extensions;
using eDoxa.Arena.Challenges.TestHelpers.Fixtures;
using eDoxa.Seedwork.Domain.Miscs;
using eDoxa.Seedwork.TestHelper;

using IdentityModel;

using Microsoft.AspNetCore.Http;

using Moq;

using Xunit;

namespace eDoxa.Arena.Challenges.UnitTests.Infrastructure.Queries
{
    public sealed class ChallengeQueryTest : UnitTest
    {
        private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;

        public ChallengeQueryTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
        }

        public static TheoryData<Game, ChallengeState> DataQueryParameters
        {
            get
            {
                var data = new TheoryData<Game, ChallengeState>();

                foreach (var state in ChallengeState.GetEnumerations())
                {
                    data.Add(Game.LeagueOfLegends, state);
                }

                return data;
            }
        }

        [Theory]
        [MemberData(nameof(DataQueryParameters))]
        public async Task FetchUserChallengeHistoryAsync_WhenChallengeQuery_ShouldBeChallenge(Game game, ChallengeState state)
        {
            //Arrange
            var challengeFaker = TestData.FakerFactory.CreateChallengeFaker(84566374, game, state);

            var challenge = challengeFaker.FakeChallenge();

            _mockHttpContextAccessor.Setup(accessor => accessor.HttpContext.User.Claims)
                .Returns(new[] {new Claim(JwtClaimTypes.Subject, challenge.Participants.First().UserId.ToString())});

            using var factory = new InMemoryDbContextFactory<ArenaChallengesDbContext>();

            using (var context = factory.CreateContext())
            {
                var challengeRepository = new ChallengeRepository(context, TestMapper);

                challengeRepository.Create(challenge);

                await challengeRepository.CommitAsync();
            }

            using (var context = factory.CreateContext())
            {
                var challengeQuery = new ChallengeQuery(context, TestMapper, _mockHttpContextAccessor.Object);

                //Act
                var challenges = await challengeQuery.FetchUserChallengeHistoryAsync(game, state);

                //Assert
                challenges.Single().Should().Be(challenge);
            }
        }

        [Theory]
        [MemberData(nameof(DataQueryParameters))]
        public async Task FetchChallengesAsync_ShouldHaveCount(Game game, ChallengeState state)
        {
            //Arrange
            var challengeFaker = TestData.FakerFactory.CreateChallengeFaker(84936374, game);

            var fakeChallenges = challengeFaker.FakeChallenges(4);

            using var factory = new InMemoryDbContextFactory<ArenaChallengesDbContext>();

            using (var context = factory.CreateContext())
            {
                var challengeRepository = new ChallengeRepository(context, TestMapper);

                challengeRepository.Create(fakeChallenges);

                await challengeRepository.CommitAsync();
            }

            using (var context = factory.CreateContext())
            {
                var challengeQuery = new ChallengeQuery(context, TestMapper, _mockHttpContextAccessor.Object);

                //Act
                var challenges = await challengeQuery.FetchChallengesAsync(game, state);

                //Assert
                challenges.Should().HaveCount(fakeChallenges.Count(challenge => challenge.Game == game && challenge.Timeline == state));
            }
        }

        [Theory]
        [MemberData(nameof(DataQueryParameters))]
        public async Task FindChallengeAsync_ShouldBeChallenge(Game game, ChallengeState state)
        {
            //Arrange
            var challengeFaker = TestData.FakerFactory.CreateChallengeFaker(84568994, game, state);

            var challenge = challengeFaker.FakeChallenge();

            using var factory = new InMemoryDbContextFactory<ArenaChallengesDbContext>();

            using (var context = factory.CreateContext())
            {
                var challengeRepository = new ChallengeRepository(context, TestMapper);

                challengeRepository.Create(challenge);

                await challengeRepository.CommitAsync();
            }

            using (var context = factory.CreateContext())
            {
                var challengeQuery = new ChallengeQuery(context, TestMapper, _mockHttpContextAccessor.Object);

                //Act
                var challengeAsync = await challengeQuery.FindChallengeAsync(challenge.Id);

                //Assert
                challengeAsync.Should().Be(challenge);
            }
        }
    }
}
