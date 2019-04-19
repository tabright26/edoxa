// Filename: ChallengeSettingsTest.cs
// Date Created: 2019-04-14
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.Factories;
using eDoxa.Challenges.Domain.ValueObjects;
using eDoxa.Testing.MSTest.Extensions;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Challenges.Domain.Tests.AggregateModels.ChallengeAggregate
{
    [TestClass]
    public sealed class ChallengeSettingsTest
    {
        private static readonly ChallengeAggregateFactory ChallengeAggregateFactory = ChallengeAggregateFactory.Instance;

        [TestMethod]
        public void Constructor_Initialize_ShouldNotThrowException()
        {
            // Arrange
            var bestOf = BestOf.Default.ToInt32();
            var entries = Entries.Default.ToInt32();
            var entryFee = EntryFee.Default.ToDecimal();
            var payoutRatio = PayoutRatio.Default.ToSingle();
            var serviceChargeRatio = ServiceChargeRatio.Default.ToSingle();

            // Act
            var settings = ChallengeAggregateFactory.CreateChallengeSettings();

            // Assert
            settings.BestOf.ToInt32().Should().Be(bestOf);
            settings.Entries.ToInt32().Should().Be(entries);
            settings.EntryFee.ToDecimal().Should().Be(entryFee);
            settings.PayoutRatio.ToSingle().Should().Be(payoutRatio);
            settings.ServiceChargeRatio.ToSingle().Should().Be(serviceChargeRatio);
        }

        [TestMethod]
        public void Type_InvalidEnumArgument_ShouldThrowInvalidEnumArgumentException()
        {
            // Arrange
            var settings = ChallengeAggregateFactory.CreateChallengeSettings();

            // Act
            var action = new Action(() => settings.SetProperty(nameof(ChallengeSettings.Type), (ChallengeType) 1000));

            // Assert
            action.Should().Throw<ArgumentException>();
        }

        [DataRow(ChallengeType.None)]
        [DataRow(ChallengeType.All)]
        [DataTestMethod]
        public void Type_InvalidArgument_ShouldThrowArgumentException(ChallengeType type)
        {
            // Arrange
            var settings = ChallengeAggregateFactory.CreateChallengeSettings();

            // Act
            var action = new Action(() => settings.SetProperty(nameof(ChallengeSettings.Type), type));

            // Assert
            action.Should().Throw<ArgumentException>();
        }

        [DataRow(Entries.MinEntries - 1)]
        [DataRow(Entries.MaxEntries + 1)]
        [DataRow(Entries.DefaultPrimitive - 5)]
        [DataTestMethod]
        public void Entries_InvalidArgument_ShouldThrowArgumentException(int entries)
        {
            // Act
            var action = new Action(() => new Entries(entries));

            // Assert
            action.Should().Throw<ArgumentException>();
        }

        [DataRow((double) EntryFee.MinEntryFee - 1)]
        [DataRow((double) EntryFee.MaxEntryFee + 1)]
        [DataRow((double) EntryFee.DefaultPrimitive - 0.01D)]
        [DataTestMethod]
        public void EntryFee_InvalidArgument_ShouldThrowArgumentException(double entryFee)
        {
            // Act
            var action = new Action(() => new EntryFee((decimal) entryFee));

            // Assert
            action.Should().Throw<ArgumentException>();
        }

        [DataRow(PayoutRatio.MinPayoutRatio - 0.1F)]
        [DataRow(PayoutRatio.MaxPayoutRatio + 0.1F)]
        [DataRow(PayoutRatio.DefaultPrimitive - 0.01F)]
        [DataTestMethod]
        public void PayoutRatio_InvalidArgument_ShouldThrowArgumentException(float payoutRatio)
        {
            // Act
            var action = new Action(() => new PayoutRatio(payoutRatio));

            // Assert
            action.Should().Throw<ArgumentException>();
        }

        [DataTestMethod]
        [DataRow(ServiceChargeRatio.MinServiceChargeRatio - 0.1F)]
        [DataRow(ServiceChargeRatio.MaxServiceChargeRatio + 0.1F)]
        [DataRow(ServiceChargeRatio.DefaultPrimitive + 0.001F)]
        public void ServiceCharge_InvalidArgument_ShouldThrowArgumentException(float serviceChargeRatio)
        {
            // Act
            var action = new Action(() => new ServiceChargeRatio(serviceChargeRatio));

            // Assert
            action.Should().Throw<ArgumentException>();
        }

        [DataRow(BestOf.MinBestOf - 1)]
        [DataRow(BestOf.MaxBestOf + 1)]
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