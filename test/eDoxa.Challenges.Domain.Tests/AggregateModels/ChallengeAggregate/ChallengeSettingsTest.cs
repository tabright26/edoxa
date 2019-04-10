// Filename: ChallengeSettingsTest.cs
// Date Created: 2019-03-18
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate.Helpers;
using eDoxa.Challenges.Domain.Factories;
using eDoxa.Testing.MSTest.Extensions;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Challenges.Domain.Tests.AggregateModels.ChallengeAggregate
{
    [TestClass]
    public sealed class ChallengeSettingsTest
    {
        private static readonly ChallengeAggregateFactory _factory = ChallengeAggregateFactory.Instance;

        [TestMethod]
        public void Constructor_Initialize_ShouldNotThrowException()
        {
            // Arrange
            const int bestOf = ChallengeSettings.DefaultBestOf;
            const int entries = ChallengeSettings.DefaultEntries;
            const decimal entryFee = ChallengeSettings.DefaultEntryFee;
            const float payoutRatio = ChallengeSettings.DefaultPayoutRatio;
            const float serviceChargeRatio = ChallengeSettings.DefaultServiceChargeRatio;

            // Act
            var settings = _factory.CreateChallengeSettings();

            // Assert
            settings.BestOf.Should().Be(bestOf);
            settings.Entries.Should().Be(entries);
            settings.EntryFee.Should().Be(entryFee);
            settings.PayoutRatio.Should().Be(payoutRatio);
            settings.ServiceChargeRatio.Should().Be(serviceChargeRatio);
        }

        [TestMethod]
        public void Type_InvalidEnumArgument_ShouldThrowInvalidEnumArgumentException()
        {
            // Arrange
            var settings = _factory.CreateChallengeSettings();

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
            var settings = _factory.CreateChallengeSettings();

            // Act
            var action = new Action(() => settings.SetProperty(nameof(ChallengeSettings.Type), type));

            // Assert
            action.Should().Throw<ArgumentException>();
        }

        [DataRow(ChallengeSettings.MinEntries - 1)]
        [DataRow(ChallengeSettings.MaxEntries + 1)]
        [DataRow(ChallengeSettings.DefaultEntries - 5)]
        [DataTestMethod]
        public void Entries_ArgumentOutOfRange_ShouldThrowArgumentOutOfRangeException(int entries)
        {
            // Arrange
            var settings = _factory.CreateChallengeSettings();

            // Act
            var action = new Action(() => settings.SetProperty(nameof(ChallengeSettings.Entries), entries));

            // Assert
            action.Should().Throw<ArgumentOutOfRangeException>();
        }

        [DataRow((double) ChallengeSettings.MinEntryFee - 1)]
        [DataRow((double) ChallengeSettings.MaxEntryFee + 1)]
        [DataRow((double) ChallengeSettings.DefaultEntryFee - 0.01D)]
        [DataTestMethod]
        public void EntryFee_ArgumentOutOfRange_ShouldThrowArgumentOutOfRangeException(double entryFee)
        {
            // Arrange
            var settings = _factory.CreateChallengeSettings();

            // Act
            var action = new Action(() => settings.SetProperty(nameof(ChallengeSettings.EntryFee), (decimal) entryFee));

            // Assert
            action.Should().Throw<ArgumentOutOfRangeException>();
        }

        [DataRow(ChallengeSettings.MinPayoutRatio - 0.1F)]
        [DataRow(ChallengeSettings.MaxPayoutRatio + 0.1F)]
        [DataRow(ChallengeSettings.DefaultPayoutRatio - 0.01F)]
        [DataTestMethod]
        public void PayoutRatio_ArgumentOutOfRange_ShouldThrowArgumentOutOfRangeException(float payoutRatio)
        {
            // Arrange
            var settings = _factory.CreateChallengeSettings();

            // Act
            var action = new Action(() => settings.SetProperty(nameof(ChallengeSettings.PayoutRatio), payoutRatio));

            // Assert
            action.Should().Throw<ArgumentOutOfRangeException>();
        }

        [DataTestMethod]
        [DataRow(ChallengeSettings.MinServiceChargeRatio - 0.1F)]
        [DataRow(ChallengeSettings.MaxServiceChargeRatio + 0.1F)]
        [DataRow(ChallengeSettings.DefaultServiceChargeRatio + 0.001F)]
        public void ServiceCharge_ArgumentOutOfRange_ShouldThrowArgumentOutOfRangeException(float serviceChargeRatio)
        {
            // Arrange
            var settings = _factory.CreateChallengeSettings();

            // Act
            var action = new Action(() => settings.SetProperty(nameof(ChallengeSettings.ServiceChargeRatio), serviceChargeRatio));

            // Assert
            action.Should().Throw<ArgumentOutOfRangeException>();
        }

        [DataRow(ChallengeSettings.MinBestOf - 1)]
        [DataRow(ChallengeSettings.MaxBestOf + 1)]
        [DataTestMethod]
        public void BestOf_ArgumentOutOfRange_ShouldThrowArgumentOutOfRangeException(int bestOf)
        {
            // Arrange
            var settings = _factory.CreateChallengeSettings();

            // Act
            var action = new Action(() => settings.SetProperty(nameof(ChallengeSettings.BestOf), bestOf));

            // Assert
            action.Should().Throw<ArgumentOutOfRangeException>();
        }

        [DataRow(50, 0.5F)]
        [DataRow(100, 0.6F)]
        [DataTestMethod]
        public void PayoutEntries_ValidData_ShouldBeValid(int entries, float payoutRatio)
        {
            // Arrange
            var helper = new ChallengeHelper();
            var settings = _factory.CreateChallengeSettings(entries: entries, payoutRatio: payoutRatio);
            
            // Act
            var payoutEntries = helper.PayoutEntries(entries, payoutRatio);

            // Assert
            settings.PayoutEntries.Should().Be(payoutEntries);
        }

        [DataRow(50, 5D, 0.2F)]
        [DataRow(100, 10D, 0.3F)]
        [DataTestMethod]
        public void PrizePool_ValidData_ShouldBeValid(int entries, double entryFee, float serviceChargeRatio)
        {
            // Arrange
            var helper = new ChallengeHelper();
            var settings = _factory.CreateChallengeSettings(entries: entries, entryFee: (decimal) entryFee, serviceChargeRatio: serviceChargeRatio);

            // Act
            var prizePool = helper.PrizePool(entries, (decimal) entryFee, serviceChargeRatio);

            // Assert
            settings.PrizePool.Should().Be(prizePool);
        }
    }
}