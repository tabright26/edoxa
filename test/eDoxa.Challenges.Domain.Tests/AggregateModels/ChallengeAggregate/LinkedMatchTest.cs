// Filename: LinkedMatchTest.cs
// Date Created: 2019-03-20
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Challenges.Domain.AggregateModels;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Challenges.Domain.Tests.AggregateModels.ChallengeAggregate
{
    [TestClass]
    public sealed class LinkedMatchTest
    {
        [TestMethod]
        public void FromGuid_ValidGuid_ShouldBeInputToString()
        {
            // Arrange
            var input = Guid.NewGuid();

            // Act
            var linkedMatch = LinkedMatch.FromGuid(input);

            // Assert
            linkedMatch.Ref.Should().Be(input.ToString());
        }

        [DataRow("9876543210")]
        [DataRow("abcdefghijklmnopqrstuvwxyz")]
        [DataRow("ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
        [DataRow("9i8h7g6f5e4d3c2b1a0")]
        [DataRow("9i8h7g-6f5e4d3_c2b1a0")]
        [DataTestMethod]
        public void Parse_ValidFormat_ShouldBe(string input)
        {
            // Act
            var linkedMatch = LinkedMatch.Parse(input);

            // Assert
            linkedMatch.Ref.Should().Be(input);
        }

        [DataRow("!@#$%^&*()")]
        [DataRow("9i8h7g 6f5e4d3 c2b1a0")]
        [DataTestMethod]
        public void Parse_InvalidFormat_ShouldThrowFormatException(string input)
        {
            // Act
            var action = new Action(() => LinkedMatch.Parse(input));

            // Assert
            action.Should().Throw<FormatException>();
        }

        [DataRow(null)]
        [DataRow("  ")]
        [DataTestMethod]
        public void Parse_InvalidArgument_ShouldThrowArgumentException(string input)
        {
            // Act
            var action = new Action(() => LinkedMatch.Parse(input));

            // Assert
            action.Should().Throw<ArgumentException>();
        }
    }
}