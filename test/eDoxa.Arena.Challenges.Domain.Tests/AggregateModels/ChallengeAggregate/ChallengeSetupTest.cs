// Filename: ChallengeSetupTest.cs
// Date Created: 2019-05-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Arena.Challenges.Tests.Factories;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.Domain.Tests.AggregateModels.ChallengeAggregate
{
    [TestClass]
    public sealed class ChallengeSetupTest
    {
        private static readonly FakeChallengeFactory FakeChallengeFactory = FakeChallengeFactory.Instance;

        [TestMethod]
        public void Constructor_Initialize_ShouldNotThrowException()
        {
            // Arrange
            var bestOf = BestOf.DefaultValue;
            var entries = Entries.DefaultValue;
            var entryFee = MoneyEntryFee.Five;
            var payoutRatio = PayoutRatio.Default;
            var serviceChargeRatio = ServiceChargeRatio.Default;

            // Act
            var setup = FakeChallengeFactory.CreateChallengeSetup();

            // Assert
            setup.BestOf.Should().Be(bestOf);
            setup.Entries.Should().Be(entries);
            setup.EntryFee.Should().Be(entryFee);
            setup.PayoutRatio.Should().Be(payoutRatio);
            setup.ServiceChargeRatio.Should().Be(serviceChargeRatio);
        }

        [DataRow(Entries.Min - 1)]
        [DataRow(Entries.Max + 1)]
        [DataRow(Entries.Default - 5)]
        [DataTestMethod]
        public void Entries_InvalidArgument_ShouldThrowArgumentException(int entries)
        {
            // Act
            var action = new Action(() => new Entries(entries));

            // Assert
            action.Should().Throw<ArgumentException>();
        }

        [DataRow(BestOf.Min - 1)]
        [DataRow(BestOf.Max + 1)]
        [DataTestMethod]
        public void BestOf_InvalidArgument_ShouldThrowArgumentException(int bestOf)
        {
            // Act
            var action = new Action(() => new BestOf(bestOf));

            // Assert
            action.Should().Throw<ArgumentException>();
        }
    }
}