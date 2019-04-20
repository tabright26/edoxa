﻿// Filename: ChallengeSettingsTest.cs
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
            var bestOf = BestOf.DefaultValue;
            var entries = Entries.DefaultValue;
            var entryFee = EntryFee.DefaultValue;
            var payoutRatio = PayoutRatio.DefaultValue;
            var serviceChargeRatio = ServiceChargeRatio.DefaultValue;

            // Act
            var settings = ChallengeAggregateFactory.CreateChallengeSettings();

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