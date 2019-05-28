// Filename: CreateChallengeCommandValidatorTest.cs
// Date Created: 2019-05-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Arena.Challenges.Application.Commands;
using eDoxa.Arena.Challenges.Application.Commands.Validations;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Domain.Aggregate;
using eDoxa.Seedwork.Domain.Enumerations;

using FluentAssertions;

using Microsoft.AspNetCore.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Arena.Challenges.Application.Tests.Commands.Validations
{
    [TestClass]
    public sealed class CreateChallengeCommandValidatorTest
    {
        private Mock<IHostingEnvironment> _mockHostingEnvironment;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockHostingEnvironment = new Mock<IHostingEnvironment>();
            _mockHostingEnvironment.SetupGet(mock => mock.EnvironmentName).Returns("Development");
        }

        [DataRow("Weekly challenge", "leagueOfLegends", 4, 3, 10, 2.5D, "money", "inProgress")]
        [DataRow("Monthly challenge", "leagueOfLegends", 1, 3, 100, 2.5D, "money", "inProgress")]
        [DataRow("Daily challenge", "leagueOfLegends", 7, 7, 20, 2.5D, "money", "inProgress")]
        [DataRow("Challenge", "leagueOfLegends", 3, 5, 15, 2500D, "token", "inProgress")]
        [DataTestMethod]
        public void Validate_CreateChallengeCommand_ShouldBeTrue(
            string name,
            string game,
            int duration,
            int bestOf,
            int payoutEntries,
            double amount,
            string currency,
            string testModeState
        )
        {
            // Arrange
            var command = new CreateChallengeCommand(
                name,
                Enumeration.FromName<Game>(game),
                duration,
                bestOf,
                payoutEntries,
                new decimal(amount),
                Enumeration.FromName<Currency>(currency),
                Enumeration.FromName<ChallengeState>(testModeState)
            );

            var validator = new CreateChallengeCommandValidator(_mockHostingEnvironment.Object);

            // Act
            var result = validator.Validate(command);

            // Assert
            result.IsValid.Should().BeTrue();
        }

        //[DataRow("Challenge", "leagueOfLegends", 1, 3, 10, 2.5D, "token", "inProgress", 1)]
        //[DataRow("Challenge", "leagueOfLegends", 1, 3, 10, 10D, "token", "inProgress", 1)]
        //[DataRow("Challenge", "leagueOfLegends", 1, 3, 10, 25D, "token", "inProgress", 1)]
        //[DataRow("Challenge", "leagueOfLegends", 1, 3, 10, 10000D, "money", "inProgress", 1)]
        //[DataRow("Challenge", "leagueOfLegends", 1, 3, 10, 25000D, "money", "inProgress", 1)]
        //[DataRow("Challenge", "leagueOfLegends", 1, 3, 10, 100000D, "money", "inProgress", 1)]
        //[DataRow("Challenge", "leagueOfLegends", 1, 3, 10, 2500D, "money", "inProgress", 1)]
        //[DataTestMethod]
        //public void Validate_CreateChallengeCommand_ShouldBeFalse(
        //    string name,
        //    string game,
        //    int duration,
        //    int bestOf,
        //    int payoutEntries,
        //    double amount,
        //    string currency,
        //    string testModeState,
        //    int errorCount
        //)
        //{
        //    // Arrange
        //    var command = new CreateChallengeCommand(
        //        name,
        //        Enumeration.FromName<Game>(game),
        //        duration,
        //        bestOf,
        //        payoutEntries,
        //        new decimal(amount),
        //        Enumeration.FromName<Currency>(currency),
        //        Enumeration.FromName<ChallengeState>(testModeState)
        //    );

        //    var validator = new CreateChallengeCommandValidator(_mockHostingEnvironment.Object);

        //    // Act
        //    var result = validator.Validate(command);

        //    // Assert
        //    result.Errors.Should().HaveCount(errorCount);
        //}
    }
}
