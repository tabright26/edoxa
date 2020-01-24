// Filename: ChallengeQueryTest.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Challenges.Api.Infrastructure.Queries;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Infrastructure;
using eDoxa.Challenges.Infrastructure.Repositories;
using eDoxa.Challenges.TestHelper;
using eDoxa.Challenges.TestHelper.Assertions.Extensions;
using eDoxa.Challenges.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.Seedwork.TestHelper;

using Xunit;

namespace eDoxa.Challenges.UnitTests.Infrastructure.Queries
{
    public sealed class ChallengeQueryTest : UnitTest
    {
        public ChallengeQueryTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator validator) : base(testData, testMapper, validator)
        {
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

            using var factory = new InMemoryDbContextFactory<ChallengesDbContext>();

            using (var context = factory.CreateContext())
            {
                var challengeRepository = new ChallengeRepository(context);

                challengeRepository.Create(challenge);

                await challengeRepository.CommitAsync();
            }

            using (var context = factory.CreateContext())
            {
                var challengeQuery = new ChallengeQuery(context);

                //Act
                var challenges = await challengeQuery.FetchUserChallengeHistoryAsync(challenge.Participants.First().UserId, game, state);

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

            using var factory = new InMemoryDbContextFactory<ChallengesDbContext>();

            using (var context = factory.CreateContext())
            {
                var challengeRepository = new ChallengeRepository(context);

                challengeRepository.Create(fakeChallenges);

                await challengeRepository.CommitAsync();
            }

            using (var context = factory.CreateContext())
            {
                var challengeQuery = new ChallengeQuery(context);

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

            using var factory = new InMemoryDbContextFactory<ChallengesDbContext>();

            using (var context = factory.CreateContext())
            {
                var challengeRepository = new ChallengeRepository(context);

                challengeRepository.Create(challenge);

                await challengeRepository.CommitAsync();
            }

            using (var context = factory.CreateContext())
            {
                var challengeQuery = new ChallengeQuery(context);

                //Act
                var challengeAsync = await challengeQuery.FindChallengeAsync(challenge.Id);

                //Assert
                challengeAsync.Should().Be(challenge);
            }
        }
    }
}
