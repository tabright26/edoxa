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

using eDoxa.Challenges.Domain.ValueObjects;

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
            // Act
            Entries.Random(new Entries(minValue), new Entries(maxValue));
        }

        [TestMethod]
        public void NextEntries_MinValueGreaterThenMaxValue_ShouldThrowArgumentException()
        {
            // Act
            var action = new Action(() => Entries.Random(new Entries(Entries.Max), new Entries(Entries.Min)));

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
            // Act
            EntryFee.Random(new EntryFee((decimal) minValue), new EntryFee((decimal) maxValue));
        }

        [TestMethod]
        public void NextEntryFee_MinValueGreaterThenMaxValue_ShouldThrowArgumentException()
        {
            // Act
            var action = new Action(() => EntryFee.Random(new EntryFee(EntryFee.Max), new EntryFee(EntryFee.Min)));

            // Assert
            action.Should().Throw<ArgumentException>();
        }

        [DataRow(1, 7)]
        [DataRow(3, 5)]
        [DataTestMethod]
        public void NextBestOf_ValidData_ShouldBeValid(int minValue, int maxValue)
        {
            // Act
            BestOf.Random(new BestOf(minValue), new BestOf(maxValue));
        }

        [TestMethod]
        public void NextBestOf_MinValueGreaterThenMaxValue_ShouldThrowArgumentException()
        {
            // Act
            var action = new Action(() => BestOf.Random(new BestOf(BestOf.Max), new BestOf(BestOf.Min)));

            // Assert
            action.Should().Throw<ArgumentException>();
        }
    }
}