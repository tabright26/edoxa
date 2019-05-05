// Filename: LinkedAccountTest.cs
// Date Created: 2019-05-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Challenges.Domain.AggregateModels.ParticipantAggregate;

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
            var linkedAccount = new LinkedAccount(input);

            // Assert
            linkedAccount.ToString().Should().Be(input.ToString());
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
            var linkedAccount = new LinkedAccount(input);

            // Assert
            linkedAccount.ToString().Should().Be(input);
        }

        [DataRow("!@#$%^&*()")]
        [DataRow("9i8h7g 6f5e4d3 c2b1a0")]
        [DataRow("  ")]
        [DataRow(null)]
        [DataTestMethod]
        public void Parse_InvalidArgument_ShouldThrowArgumentException(string input)
        {
            // Act
            var action = new Action(() => new LinkedAccount(input));

            // Assert
            action.Should().Throw<ArgumentException>();
        }
    }
}