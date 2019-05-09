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

using eDoxa.Challenges.Domain.Entities.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.Factories;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Challenges.Domain.Entities.Tests.AggregateModels.ChallengeAggregate
{
    [TestClass]
    public sealed class ChallengeSetupTest
    {
        private static readonly FakeDefaultChallengeFactory FakeDefaultChallengeFactory = FakeDefaultChallengeFactory.Instance;

        [TestMethod]
        public void Constructor_Initialize_ShouldNotThrowException()
        {
            // Arrange
            var bestOf = BestOf.DefaultValue;
            var entries = Entries.DefaultValue;
            var entryFee = EntryFee.DefaultValue;
            var payoutRatio = PayoutRatio.DefaultValue;
            var serviceChargeRatio = ServiceChargeRatio.DefaultValue;

            // Act
            var setup = FakeDefaultChallengeFactory.CreateChallengeSetup(ChallengeType.All);

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

        [DataRow((double) EntryFee.Min - 1)]
        [DataRow((double) EntryFee.Max + 1)]
        [DataRow((double) EntryFee.Default - 0.01D)]
        [DataTestMethod]
        public void EntryFee_InvalidArgument_ShouldThrowArgumentException(double entryFee)
        {
            // Act
            var action = new Action(() => new EntryFee((decimal) entryFee));

            // Assert
            action.Should().Throw<ArgumentException>();
        }

        [DataRow(PayoutRatio.Min - 0.1F)]
        [DataRow(PayoutRatio.Max + 0.1F)]
        [DataRow(PayoutRatio.Default - 0.01F)]
        [DataTestMethod]
        public void PayoutRatio_InvalidArgument_ShouldThrowArgumentException(float payoutRatio)
        {
            // Act
            var action = new Action(() => new PayoutRatio(payoutRatio));

            // Assert
            action.Should().Throw<ArgumentException>();
        }

        [DataTestMethod]
        [DataRow(ServiceChargeRatio.Min - 0.1F)]
        [DataRow(ServiceChargeRatio.Max + 0.1F)]
        [DataRow(ServiceChargeRatio.Default + 0.001F)]
        public void ServiceCharge_InvalidArgument_ShouldThrowArgumentException(float serviceChargeRatio)
        {
            // Act
            var action = new Action(() => new ServiceChargeRatio(serviceChargeRatio));

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