// Filename: ParticipantQueryTest.cs
// Date Created: 2019-09-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Api.Infrastructure.Queries;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Infrastructure;
using eDoxa.Arena.Challenges.Infrastructure.Repositories;
using eDoxa.Arena.Challenges.TestHelper;
using eDoxa.Arena.Challenges.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain.Miscs;
using eDoxa.Seedwork.TestHelper;

using FluentAssertions;

using Xunit;

namespace eDoxa.Arena.Challenges.UnitTests.Infrastructure.Queries
{
    public sealed class ParticipantQueryTest : UnitTest
    {
        public ParticipantQueryTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
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
        public async Task FindChallengeParticipantsAsync_ShouldBeEquivalentToParticipantList(Game game, ChallengeState state)
        {
            //Arrange
            var challengeFaker = TestData.FakerFactory.CreateChallengeFaker(68545632, game, state);

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
                var participantQuery = new ParticipantQuery(context, TestMapper);

                //Act
                var participants = await participantQuery.FetchChallengeParticipantsAsync(challenge.Id);

                //Assert
                participants.Should().BeEquivalentTo(challenge.Participants.ToList());
            }
        }

        [Theory]
        [MemberData(nameof(DataQueryParameters))]
        public async Task FindParticipantAsync_EquivalentToParticipant(Game game, ChallengeState state)
        {
            //Arrange
            var challengeFaker = TestData.FakerFactory.CreateChallengeFaker(48956632, game, state);

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
                var participantQuery = new ParticipantQuery(context, TestMapper);

                foreach (var participant in challenge.Participants)
                {
                    //Act
                    var participantAsync = await participantQuery.FindParticipantAsync(ParticipantId.FromGuid(participant.Id));

                    //Assert
                    participantAsync.Should().BeEquivalentTo(participant);
                }
            }
        }
    }
}
