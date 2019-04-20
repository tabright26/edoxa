// Filename: ParticipantLinkedAccountTest.cs
// Date Created: 2019-03-05
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
    public sealed class LinkedAccountTest
    {
        [TestMethod]
        public void FromGuid_ValidGuid_ShouldBeInput()
        {
            // Arrange
            var input = Guid.NewGuid();

            // Act
            var linkedAccount = LinkedAccount.FromGuid(input);

            // Assert
            linkedAccount.Ref.Should().Be(input.ToString());
        }

        [DataTestMethod]
        [DataRow("9876543210")]
        [DataRow("abcdefghijklmnopqrstuvwxyz")]
        [DataRow("ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
        [DataRow("9i8h7g6f5e4d3c2b1a0")]
        [DataRow("9i8h7g-6f5e4d3_c2b1a0")]
        public void Parse_ValidFormat_ShouldBe(string input)
        {
            // Act
            var linkedAccount = LinkedAccount.Parse(input);

            // Assert
            linkedAccount.Ref.Should().Be(input);
        }

        [DataTestMethod]
        [DataRow("!@#$%^&*()")]
        [DataRow("9i8h7g 6f5e4d3 c2b1a0")]
        public void Parse_InvalidFormat_ShouldThrowFormatException(string input)
        {
            // Act
            var action = new Action(() => LinkedAccount.Parse(input));

            // Assert
            action.Should().Throw<FormatException>();
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("  ")]
        public void Parse_InvalidArgument_ShouldThrowArgumentException(string input)
        {
            // Act
            var action = new Action(() => LinkedAccount.Parse(input));

            // Assert
            action.Should().Throw<ArgumentException>();
        }
    }
}