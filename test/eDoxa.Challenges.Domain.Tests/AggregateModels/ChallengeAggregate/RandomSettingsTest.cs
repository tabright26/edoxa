// Filename: RandomSettingsTest.cs
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
            var random = new EntriesRandom();

            // Act
            random.Next(new EntriesRange(new Entries(minValue), new Entries(maxValue)));
        }

        [TestMethod]
        public void NextEntries_MinValueGreaterThenMaxValue_ShouldThrowArgumentException()
        {
            // Arrange
            var random = new EntriesRandom();

            // Act
            var action = new Action(() => random.Next(new EntriesRange(new Entries(Entries.Max), new Entries(Entries.Min))));

            // Assert
            action.Should().Throw<ArgumentException>();
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
            var random = new EntryFeeRandom();

            // Act
            random.Next(new EntryFeeRange(new EntryFee((decimal) minValue), new EntryFee((decimal) maxValue)));
        }

        [TestMethod]
        public void NextEntryFee_MinValueGreaterThenMaxValue_ShouldThrowArgumentException()
        {
            // Arrange
            var random = new EntryFeeRandom();

            // Act
            var action = new Action(() => random.Next(new EntryFeeRange(new EntryFee(EntryFee.Max), new EntryFee(EntryFee.Min))));

            // Assert
            action.Should().Throw<ArgumentException>();
        }

        [DataRow(1, 7)]
        [DataRow(3, 5)]
        [DataTestMethod]
        public void NextBestOf_ValidData_ShouldBeValid(int minValue, int maxValue)
        {
            // Arrange
            var random = new BestOfRandom();

            // Act
            random.Next(new BestOfRange(new BestOf(minValue), new BestOf(maxValue)));
        }

        [TestMethod]
        public void NextBestOf_MinValueGreaterThenMaxValue_ShouldThrowArgumentException()
        {
            // Arrange
            var random = new BestOfRandom();

            // Act
            var action = new Action(() => random.Next(new BestOfRange(new BestOf(BestOf.Max), new BestOf(BestOf.Min))));

            // Assert
            action.Should().Throw<ArgumentException>();
        }
    }
}