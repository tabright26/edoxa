// Filename: ChallengeNameTest.cs
// Date Created: 2019-03-21
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.Factories;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Challenges.Domain.Tests.AggregateModels.ChallengeAggregate
{
    [TestClass]
    public sealed class ChallengeNameTest
    {
        private static readonly ChallengeAggregateFactory ChallengeAggregateFactory = ChallengeAggregateFactory.Instance;

        [DataRow("Challenge")]
        [DataRow("Challenge 1")]
        [DataRow("Challenge (1)")]
        [DataRow("Challenge one")]
        [DataRow("Challenge")]
        [DataTestMethod]
        public void Value_ValidFormat_ShouldBeValue(string input)
        {
            // Act
            var name = ChallengeAggregateFactory.CreateChallengeName(input);

            // Assert
            name.ToString().Should().Be(input);
        }

        [DataRow("challenge_name")]
        [DataRow("!@#$!@*&")]
        [DataTestMethod]
        public void Value_InvalidFormat_ShouldThrowFormatException(string input)
        {
            // Act
            var action = new Action(() => ChallengeAggregateFactory.CreateChallengeName(input));

            // Assert
            action.Should().Throw<FormatException>();
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("  ")]
        public void Value_InvalidArgument_ShouldThrowArgumentException(string input)
        {
            // Act
            var action = new Action(() => ChallengeAggregateFactory.CreateChallengeName(input));

            // Assert
            action.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void Operator_String_ShouldBeInput()
        {
            // Arrange
            var name = ChallengeAggregateFactory.CreateChallengeName();

            // Act
            string input = name;

            // Assert
            name.ToString().Should().Be(input);
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