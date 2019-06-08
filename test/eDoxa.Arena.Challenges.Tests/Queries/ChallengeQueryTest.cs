using System;

using AutoMapper;

using eDoxa.Arena.Challenges.Application.ViewModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Services.Factories;
using eDoxa.Arena.Challenges.Tests.Utilities.Asserts;
using eDoxa.Seedwork.Domain.Common.Enumerations;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.Tests.Queries
{
    [TestClass]
    public sealed class ChallengeQueryTest
    {
        [TestMethod]
        public void M()
        {
            // Arrange
            var mapper = CreateMapper();

            var challenge = CreateChallenge();

            // Act
            var challengeDTO = mapper.Map<ChallengeViewModel>(challenge);

            // Assert
            ChallengeQueryAssert.IsMapped(challengeDTO);
        }

        private static IMapper CreateMapper()
        {
            var services = new ServiceCollection();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            var provider = services.BuildServiceProvider();

            return provider.GetService<IMapper>();
        }

        private static Challenge CreateChallenge()
        {
            var builder = new ChallengeBuilder(
                Game.LeagueOfLegends,
                new ChallengeName("Weekly challenge"),
                new ChallengeSetup(BestOf.Three, PayoutEntries.Ten, MoneyEntryFee.Ten, new Entries(20)),
                new ChallengeTimeline(ChallengeDuration.OneDay)
            );

            builder.StoreScoring(new ScoringFactory());

            builder.StorePayout(new PayoutFactory());

            builder.EnableTestMode(new TestMode(ChallengeState.InProgress, TestModeMatchQuantity.Exact, TestModeParticipantQuantity.Fulfilled));

            return builder.Build() as Challenge;
        }
    }
}
