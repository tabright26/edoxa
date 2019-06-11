// Filename: MatchFakerTest.cs
// Date Created: 2019-06-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;
using System.Linq;

using eDoxa.Arena.Challenges.Domain.Fakers;
using eDoxa.Seedwork.Common.Enumerations;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.UnitTests.Application.Data.Fakers
{
    [TestClass]
    public sealed class MatchFakerTest
    {
        public static IEnumerable<object[]> Data => Game.GetAll().Select(game => new object[] {game});

        [TestMethod]
        public void FakeMatches_ShouldNotThrow()
        {
            // Arrange
            var matchFaker = new MatchFaker();

            // Act
            var action = new Action(
                () =>
                {
                    var matches = matchFaker.FakeMatches(5);

                    matches.Select(match => match.Reference).Distinct().Should().HaveCount(5);
                }
            );

            // Assert
            action.Should().NotThrow();
        }

        [DataTestMethod]
        [DynamicData(nameof(Data))]
        public void FakeMatches_ShouldNotThrow(Game game)
        {
            // Arrange
            var matchFaker = new MatchFaker();

            // Act
            var action = new Action(
                () =>
                {
                    var matches = matchFaker.FakeMatches(5, game);

                    matches.Select(match => match.Reference).Distinct().Should().HaveCount(5);
                }
            );

            // Assert
            action.Should().NotThrow();
        }

        [TestMethod]
        public void FakeMatch_ShouldNotThrow()
        {
            // Arrange
            var matchFaker = new MatchFaker();

            // Act
            var action = new Action(() => matchFaker.FakeMatch());

            // Assert
            action.Should().NotThrow();
        }

        [DataTestMethod]
        [DynamicData(nameof(Data))]
        public void FakeMatch_ShouldNotThrow(Game game)
        {
            // Arrange
            var matchFaker = new MatchFaker();

            // Act
            var action = new Action(() => matchFaker.FakeMatch(game));

            // Assert
            action.Should().NotThrow();
        }
    }
}
