// Filename: MatchQueryTest.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Api.Infrastructure.Data.Fakers;
using eDoxa.Arena.Challenges.Api.Infrastructure.Queries;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Infrastructure;
using eDoxa.Arena.Challenges.Infrastructure.Repositories;
using eDoxa.Arena.Challenges.UnitTests.Helpers.Extensions;
using eDoxa.Seedwork.Testing;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.UnitTests.Infrastructure.Queries
{
    [TestClass]
    public sealed class MatchQueryTest
    {
        private static IEnumerable<object[]> DataQueryParameters =>
            ChallengeGame.GetEnumerations().SelectMany(game => ChallengeState.GetEnumerations().Select(state => new object[] {game, state})).ToList();

        [DataTestMethod]
        [DynamicData(nameof(DataQueryParameters))]
        public async Task FindParticipantMatchesAsync_ShouldBeEquivalentToParticipantMatchList(ChallengeGame game, ChallengeState state)
        {
            //Arrange
            var challengeFaker = new ChallengeFaker(game, state);

            challengeFaker.UseSeed(89568322);

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
        }

        [DataTestMethod]
        [DynamicData(nameof(DataQueryParameters))]
        public async Task FindMatchAsync_ShouldBeEquivalentToMatch(ChallengeGame game, ChallengeState state)
        {
            //Arrange
            var challengeFaker = new ChallengeFaker(game, state);

            challengeFaker.UseSeed(83459632);

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
}
