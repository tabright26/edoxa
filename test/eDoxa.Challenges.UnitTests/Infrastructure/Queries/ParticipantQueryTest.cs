// Filename: ParticipantQueryTest.cs
// Date Created: 2019-09-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

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
    public sealed class ParticipantQueryTest : UnitTest
    {
        public ParticipantQueryTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator validator) : base(testData, testMapper, validator)
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

            using var factory = new InMemoryDbContextFactory<ChallengesDbContext>();

            using (var context = factory.CreateContext())
            {
                var challengeRepository = new ChallengeRepository(context);

                challengeRepository.Create(challenge);

                await challengeRepository.CommitAsync();
            }

            using (var context = factory.CreateContext())
            {
                var participantQuery = new ParticipantQuery(context);

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

            using var factory = new InMemoryDbContextFactory<ChallengesDbContext>();

            using (var context = factory.CreateContext())
            {
                var challengeRepository = new ChallengeRepository(context);

                challengeRepository.Create(challenge);

                await challengeRepository.CommitAsync();
            }

            using (var context = factory.CreateContext())
            {
                var participantQuery = new ParticipantQuery(context);

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
