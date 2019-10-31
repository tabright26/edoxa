﻿// Filename: MatchQueryTest.cs
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
using eDoxa.Arena.Challenges.TestHelpers;
using eDoxa.Arena.Challenges.TestHelpers.Fixtures;
using eDoxa.Seedwork.Domain.Miscs;
using eDoxa.Seedwork.TestHelper;

using FluentAssertions;

using Xunit;

namespace eDoxa.Arena.Challenges.UnitTests.Infrastructure.Queries
{
    public sealed class MatchQueryTest : UnitTest
    {
        public MatchQueryTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
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

            using var factory = new InMemoryDbContextFactory<ArenaChallengesDbContext>();

            using (var context = factory.CreateContext())
            {
                var challengeRepository = new ChallengeRepository(context, TestMapper);

                challengeRepository.Create(challenge);

                await challengeRepository.CommitAsync();
            }

            using (var context = factory.CreateContext())
            {
                var matchQuery = new MatchQuery(context, TestMapper);

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

            using var factory = new InMemoryDbContextFactory<ArenaChallengesDbContext>();

            using (var context = factory.CreateContext())
            {
                var challengeRepository = new ChallengeRepository(context, TestMapper);

                challengeRepository.Create(challenge);

                await challengeRepository.CommitAsync();
            }

            using (var context = factory.CreateContext())
            {
                var matchQuery = new MatchQuery(context, TestMapper);

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
