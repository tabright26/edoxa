using System;

using AutoMapper;

using eDoxa.Arena.Challenges.Domain;
using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.DTO;
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
            var challengeDTO = mapper.Map<ChallengeDTO>(challenge);

            // Assert
            ChallengeQueryAssert.IsMapped(challengeDTO);
        }

        private static IMapper CreateMapper()
        {
            var services = new ServiceCollection();

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            services.AddAutoMapper(assemblies);

            var provider = services.BuildServiceProvider();

            return provider.GetService<IMapper>();
        }

        private static Challenge CreateChallenge()
        {
            var builder = new ChallengeBuilder(
                Game.LeagueOfLegends,
                new ChallengeName("Weekly challenge"),
                new ChallengeSetup(BestOf.Three, PayoutEntries.Ten, MoneyEntryFee.Ten),
                new ChallengeTimeline(ChallengeDuration.OneDay)
            );

            builder.StoreScoring(ScoringFactory.Instance);

            builder.StorePayout(PayoutFactory.Instance);

            builder.EnableTestMode(new TestMode(ChallengeState.InProgress, TestModeMatchQuantity.Exact, TestModeParticipantQuantity.Fulfilled));

            return builder.Build() as Challenge;
        }
    }
}
