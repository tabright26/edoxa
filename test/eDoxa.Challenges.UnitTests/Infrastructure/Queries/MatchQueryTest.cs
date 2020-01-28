// Filename: MatchQueryTest.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Infrastructure;
using eDoxa.Challenges.Infrastructure.Queries;
using eDoxa.Challenges.Infrastructure.Repositories;
using eDoxa.Challenges.TestHelper;
using eDoxa.Challenges.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.Seedwork.TestHelper;

using FluentAssertions;

using Xunit;

namespace eDoxa.Challenges.UnitTests.Infrastructure.Queries
{
    public sealed class MatchQueryTest : UnitTest
    {
        public MatchQueryTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator validator) : base(testData, testMapper, validator)
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
        public async Task FindParticipantMatchesAsync_ShouldBeEquivalentToParticipantMatchList(Game game, ChallengeState state)
        {
            //Arrange
            var challengeFaker = TestData.FakerFactory.CreateChallengeFaker(89568322, game, state);

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
                var matchQuery = new MatchQuery(context);

                foreach (var participant in challenge.Participants)
                {
                    //Act
                    var matches = await matchQuery.FetchParticipantMatchesAsync(ParticipantId.FromGuid(participant.Id));

                    //Arrange
                    matches.Should().BeEquivalentTo(participant.Matches.ToList());
                }
            }
        }

        [Theory]
        [MemberData(nameof(DataQueryParameters))]
        public async Task FindMatchAsync_ShouldBeEquivalentToMatch(Game game, ChallengeState state)
        {
            //Arrange
            var challengeFaker = TestData.FakerFactory.CreateChallengeFaker(83459632, game, state);

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
                var matchQuery = new MatchQuery(context);

                foreach (var match in challenge.Participants.SelectMany(participant => participant.Matches).ToList())
                {
                    //Act
                    var matchAsync = await matchQuery.FindMatchAsync(MatchId.FromGuid(match.Id));

                    //Arrange
                    matchAsync.Should().BeEquivalentTo(match);
                }
            }
        }
    }
}
