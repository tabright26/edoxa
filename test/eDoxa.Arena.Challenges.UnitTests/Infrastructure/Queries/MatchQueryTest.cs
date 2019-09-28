// Filename: MatchQueryTest.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Api.Infrastructure.Queries;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Infrastructure;
using eDoxa.Arena.Challenges.Infrastructure.Repositories;
using eDoxa.Arena.Challenges.UnitTests.Helpers;
using eDoxa.Arena.Challenges.UnitTests.Helpers.Extensions;
using eDoxa.Seedwork.Testing;

using FluentAssertions;

using Xunit;

namespace eDoxa.Arena.Challenges.UnitTests.Infrastructure.Queries
{
    public sealed class MatchQueryTest : UnitTest
    {
        public MatchQueryTest(ChallengeFakerFixture challengeFaker) : base(challengeFaker)
        {
        }

        public static IEnumerable<object[]> DataQueryParameters =>
            ChallengeGame.GetEnumerations().SelectMany(game => ChallengeState.GetEnumerations().Select(state => new object[] {game, state})).ToList();

        [Theory]
        [MemberData(nameof(DataQueryParameters))]
        public async Task FindParticipantMatchesAsync_ShouldBeEquivalentToParticipantMatchList(ChallengeGame game, ChallengeState state)
        {
            //Arrange
            var challengeFaker = ChallengeFaker.Factory.CreateFaker(89568322, game, state);

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
                var matchQuery = new MatchQuery(context, MapperExtensions.Mapper);

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
        public async Task FindMatchAsync_ShouldBeEquivalentToMatch(ChallengeGame game, ChallengeState state)
        {
            //Arrange
            var challengeFaker = ChallengeFaker.Factory.CreateFaker(83459632, game, state);

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
                var matchQuery = new MatchQuery(context, MapperExtensions.Mapper);

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
