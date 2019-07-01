// Filename: MatchQueryTest.cs
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
    public sealed class MatchQueryTest
    {
        private static IEnumerable<object[]> DataQueryParameters =>
            ChallengeGame.GetEnumerations().SelectMany(game => ChallengeState.GetEnumerations().Select(state => new object[] {game, state})).ToList();

        [DataTestMethod]
        [DynamicData(nameof(DataQueryParameters))]
        public async Task FindParticipantMatchesAsync(ChallengeGame game, ChallengeState state)
        {
            var challengeFaker = new ChallengeFaker(game, state);

            challengeFaker.UseSeed(89568322);

            var challenge = challengeFaker.Generate();

            var challengeViewModel = challenge.ToViewModel();

            using (var factory = new InMemoryDbContextFactory<ChallengesDbContext>())
            {
                using (var context = factory.CreateContext())
                {
                    context.Challenges.Add(challenge.ToModel());

                    await context.SaveChangesAsync();
                }

                using (var context = factory.CreateContext())
                {
                    var matchQuery = new MatchQuery(context, MapperExtensions.Mapper);

                    foreach (var participant in challengeViewModel.Participants)
                    {
                        var matchViewModels = await matchQuery.FindParticipantMatchesAsync(ParticipantId.FromGuid(participant.Id));

                        matchViewModels.Should().BeEquivalentTo(participant.Matches.ToList());
                    }
                }
            }
        }

        [DataTestMethod]
        [DynamicData(nameof(DataQueryParameters))]
        public async Task FindMatchAsync(ChallengeGame game, ChallengeState state)
        {
            var challengeFaker = new ChallengeFaker(game, state);

            challengeFaker.UseSeed(83459632);

            var challenge = challengeFaker.Generate();

            var challengeViewModel = challenge.ToViewModel();

            using (var factory = new InMemoryDbContextFactory<ChallengesDbContext>())
            {
                using (var context = factory.CreateContext())
                {
                    context.Challenges.Add(challenge.ToModel());

                    await context.SaveChangesAsync();
                }

                using (var context = factory.CreateContext())
                {
                    var matchQuery = new MatchQuery(context, MapperExtensions.Mapper);

                    foreach (var match in challengeViewModel.Participants.SelectMany(participant => participant.Matches).ToList())
                    {
                        var matchViewModel = await matchQuery.FindMatchAsync(MatchId.FromGuid(match.Id));

                        matchViewModel.Should().BeEquivalentTo(match);
                    }
                }
            }
        }
    }
}
