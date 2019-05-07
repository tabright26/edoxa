// Filename: ChallengeNameTest.cs
// Date Created: 2019-05-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Challenges.Domain.Entities.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.Factories;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Challenges.Domain.Entities.Tests.AggregateModels.ChallengeAggregate
{
    [TestClass]
    public sealed class ChallengeNameTest
    {
        [DataRow("Challenge")]
        [DataRow("Challenge 1")]
        [DataRow("Challenge (1)")]
        [DataRow("Challenge one")]
        [DataRow("Challenge")]
        [DataTestMethod]
        public void Value_ValidFormat_ShouldBeValue(string input)
        {
            // Act
            var name = new ChallengeName(input);

            // Assert
            name.ToString().Should().Be(input);
        }

        [DataRow(null)]
        [DataRow("  ")]
        [DataRow("challenge_name")]
        [DataRow("!@#$!@*&")]
        [DataTestMethod]
        public void Value_InvalidArgument_ShouldThrowArgumentException(string input)
        {
            // Act
            var action = new Action(() => new ChallengeName(input));

            // Assert
            action.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void Operator_ChallengeName_ShouldBeInput()
        {
            // Arrange
            const string input = nameof(Challenge);

            // Act
            ChallengeName name = input;

            // Assert
            name.ToString().Should().Be(input);
        }
    }
}