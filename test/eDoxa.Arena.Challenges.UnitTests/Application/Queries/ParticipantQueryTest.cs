// Filename: ParticipantQueryTest.cs
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

namespace eDoxa.Arena.Challenges.UnitTests.Application.Queries
{
    [TestClass]
    public sealed class ParticipantQueryTest
    {
        private static IEnumerable<object[]> DataQueryParameters =>
            ChallengeGame.GetEnumerations().SelectMany(game => ChallengeState.GetEnumerations().Select(state => new object[] {game, state})).ToList();

        [DataTestMethod]
        [DynamicData(nameof(DataQueryParameters))]
        public async Task FindChallengeParticipantsAsync_ShouldBeEquivalentToParticipantList(ChallengeGame game, ChallengeState state)
        {
            //Arrange
            var challengeFaker = new ChallengeFaker(game, state);

            challengeFaker.UseSeed(68545632);

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
                    var participantQuery = new ParticipantQuery(context, MapperExtensions.Mapper);

                    //Act
                    var participantViewModels = await participantQuery.FetchChallengeParticipantsAsync(challenge.Id);

                    //Assert
                    participantViewModels.Should().BeEquivalentTo(challenge.Participants.ToList());
                }
            }
        }

        [DataTestMethod]
        [DynamicData(nameof(DataQueryParameters))]
        public async Task FindParticipantAsync_EquivalentToParticipant(ChallengeGame game, ChallengeState state)
        {
            //Arrange
            var challengeFaker = new ChallengeFaker(game, state);

            challengeFaker.UseSeed(48956632);

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
                    var participantQuery = new ParticipantQuery(context, MapperExtensions.Mapper);

                    foreach (var participant in challenge.Participants)
                    {
                        //Act
                        var participantViewModel = await participantQuery.FindParticipantAsync(ParticipantId.FromGuid(participant.Id));

                        //Assert
                        participantViewModel.Should().BeEquivalentTo(participant);
                    }
                }
            }
        }
    }
}
