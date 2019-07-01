// Filename: ParticipantQueryTest.cs
// Date Created: 2019-06-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Api.Application.Fakers;
using eDoxa.Arena.Challenges.Api.Application.Queries;
using eDoxa.Arena.Challenges.Api.Extensions;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Infrastructure;
using eDoxa.Seedwork.Infrastructure.Factories;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.UnitTests.Application.Queries
{
    [TestClass]
    public sealed class ParticipantQueryTest
    {
        private static IEnumerable<object[]> DataQueryParameters =>
            ChallengeGame.GetEnumerations().SelectMany(game => ChallengeState.GetEnumerations().Select(state => new object[] { game, state })).ToList();

        [DataTestMethod]
        [DynamicData(nameof(DataQueryParameters))]
        public async Task FindChallengeParticipantsAsync(ChallengeGame game, ChallengeState state)
        {
            var challengeFaker = new ChallengeFaker(game, state);

            challengeFaker.UseSeed(68545632);

            var challenge = challengeFaker.Generate();

            using (var factory = new InMemoryDbContextFactory<ChallengesDbContext>())
            {
                using (var context = factory.CreateContext())
                {
                    context.Challenges.Add(challenge.ToModel());

                    await context.SaveChangesAsync();
                }

                using (var context = factory.CreateContext())
                {
                    var participantQuery = new ParticipantQuery(context, MapperExtensions.Mapper);

                    var participantViewModels = await participantQuery.FindChallengeParticipantsAsync(challenge.Id);

                    var participants = challenge.Participants.ToViewModels();

                    participantViewModels.Should().BeEquivalentTo(participants);
                }
            }
        }

        [DataTestMethod]
        [DynamicData(nameof(DataQueryParameters))]
        public async Task FindParticipantAsync(ChallengeGame game, ChallengeState state)
        {
            var challengeFaker = new ChallengeFaker(game, state);

            challengeFaker.UseSeed(48956632);

            var challenge = challengeFaker.Generate();

            using (var factory = new InMemoryDbContextFactory<ChallengesDbContext>())
            {
                using (var context = factory.CreateContext())
                {
                    context.Challenges.Add(challenge.ToModel());

                    await context.SaveChangesAsync();
                }

                using (var context = factory.CreateContext())
                {
                    var participantQuery = new ParticipantQuery(context, MapperExtensions.Mapper);

                    foreach (var participant in challenge.Participants)
                    {
                        var participantViewModel = await participantQuery.FindParticipantAsync(participant.Id);

                        participantViewModel.Should().BeEquivalentTo(participant.ToViewModel());
                    }
                }
            }
        }
    }
}
