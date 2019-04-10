// Filename: ChallengeSettingsHelperTest.cs
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

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Challenges.Domain.Tests.AggregateModels.ChallengeAggregate
{
    [TestClass]
    public sealed class RandomSettingsTest
    {
        [DataRow(30, 100)]
        [DataRow(100, 1000)]
        [DataRow(1000, 2000)]
        [DataTestMethod]
        public void NextEntries_ValidData_ShouldBeValid(int minValue, int maxValue)
        {
            // Arrange
            var random = new RandomSettings();

            // Act
            var entries = random.NextEntries(minValue, maxValue);

            // Assert
            entries.Should().BeGreaterOrEqualTo(ChallengeSettings.MinEntries);
            entries.Should().BeLessOrEqualTo(ChallengeSettings.MaxEntries);
            (entries % 10 == 0).Should().BeTrue();
        }

        [TestMethod]
        public void NextEntries_MinValueGreaterThenMaxValue_ShouldThrowArgumentOutOfRangeException()
        {
            // Arrange
            var random = new RandomSettings();

            // Act
            var action = new Action(() => random.NextEntries(ChallengeSettings.MaxEntries, ChallengeSettings.MinEntries));

            // Assert
            action.Should().Throw<ArgumentOutOfRangeException>();
        }

        [DataRow(0.25D, 0.25D)]
        [DataRow(0.25D, 5D)]
        [DataRow(0.25D, 100D)]
        [DataRow(0.25D, 1000D)]
        [DataRow(4.75D, 4.75D)]
        [DataRow(5D, 50D)]
        [DataRow(100D, 1000D)]
        [DataRow(1000D, 1500D)]
        [DataTestMethod]
        public void NextEntryFee_ValidData_ShouldBeValid(double minValue, double maxValue)
        {
            // Arrange
            var random = new RandomSettings();

            // Act
            var entryFee = random.NextEntryFee((decimal) minValue, (decimal) maxValue);

            // Assert
            entryFee.Should().BeGreaterOrEqualTo(ChallengeSettings.MinEntryFee);
            entryFee.Should().BeLessOrEqualTo(ChallengeSettings.MaxEntryFee);
            (entryFee % 0.25M == 0).Should().BeTrue();
        }

        [TestMethod]
        public void NextEntryFee_MinValueGreaterThenMaxValue_ShouldThrowArgumentOutOfRangeException()
        {
            // Arrange
            var random = new RandomSettings();

            // Act
            var action = new Action(() => random.NextEntryFee(ChallengeSettings.MaxEntryFee, ChallengeSettings.MinEntryFee));

            // Assert
            action.Should().Throw<ArgumentOutOfRangeException>();
        }

        [DataRow(1, 7)]
        [DataRow(3, 5)]
        [DataTestMethod]
        public void NextBestOf_ValidData_ShouldBeValid(int minValue, int maxValue)
        {
            // Arrange
            var random = new RandomSettings();

            // Act
            var bestOf = random.NextBestOf(minValue, maxValue);

            // Assert
            bestOf.Should().BeGreaterOrEqualTo(ChallengeSettings.MinBestOf);
            bestOf.Should().BeLessOrEqualTo(ChallengeSettings.MaxBestOf);
            (bestOf >= 1 || bestOf <= 7).Should().BeTrue();
        }

        [TestMethod]
        public void NextBestOf_MinValueGreaterThenMaxValue_ShouldThrowArgumentOutOfRangeException()
        {
            // Arrange
            var random = new RandomSettings();

            // Act
            var action = new Action(() => random.NextBestOf(ChallengeSettings.MaxBestOf, ChallengeSettings.MinBestOf));

            // Assert
            action.Should().Throw<ArgumentOutOfRangeException>();
        }
    }
}