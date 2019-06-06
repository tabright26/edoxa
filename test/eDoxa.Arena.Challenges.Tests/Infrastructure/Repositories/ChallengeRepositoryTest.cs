// Filename: ChallengeRepositoryTest.cs
// Date Created: 2019-06-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Infrastructure;
using eDoxa.Arena.Challenges.Infrastructure.Repositories;
using eDoxa.Arena.Challenges.Services.Factories;
using eDoxa.Seedwork.Domain.Common.Enumerations;
using eDoxa.Seedwork.Infrastructure.Factories;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.Tests.Infrastructure.Repositories
{
    [TestClass]
    public sealed class ChallengeRepositoryTest
    {
        [TestMethod]
        public async Task Create_Challenge_ShouldNotBeNull()
        {
            var challenge = CreateChallenge();

            using (var factory = new InMemoryDbContextFactory<ChallengesDbContext>())
            {
                using (var context = factory.CreateContext())
                {
                    var repository = new ChallengeRepository(context);

                    repository.Create(challenge);

                    await repository.UnitOfWork.CommitAsync();
                }

                using (var context = factory.CreateContext())
                {
                    var repository = new ChallengeRepository(context);

                    challenge = await repository.FindChallengeAsync(challenge.Id);

                    var data = new ChallengeData(challenge);

                    challenge.Should().NotBeNull();
                }
            }
        }

        private static Challenge CreateChallenge()
        {
            var builder = new ChallengeBuilder(
                Game.LeagueOfLegends,
                new ChallengeName("Weekly challenge"),
                new ChallengeSetup(BestOf.Three, PayoutEntries.Ten, MoneyEntryFee.Ten, new Entries(20)),
                new ChallengeTimeline(ChallengeDuration.OneDay)
            );

            builder.StoreScoring(ScoringFactory.Instance);

            builder.StorePayout(PayoutFactory.Instance);

            builder.EnableTestMode(new TestMode(ChallengeState.InProgress, TestModeMatchQuantity.Exact, TestModeParticipantQuantity.Fulfilled));

            return builder.Build() as Challenge;
        }
    }
}
