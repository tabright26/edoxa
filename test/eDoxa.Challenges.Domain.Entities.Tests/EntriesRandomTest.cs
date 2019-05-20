// Filename: EntriesRandomTest.cs
// Date Created: 2019-05-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Challenges.Domain.Entities.Tests
{
    [TestClass]
    public sealed class EntriesRandomTest
    {
        private static readonly EntriesRandom Random = new EntriesRandom();

        [DataRow(30, 100)]
        [DataRow(100, 1000)]
        [DataRow(1000, 2000)]
        [DataTestMethod]
        public void Next_Entries_ShouldBeInRange(int minValue, int maxValue)
        {
            // Arrange
            var minimumValue = new Entries(minValue);
            var maximumValue = new Entries(maxValue);

            // Act
            var entries = Random.Next(new EntriesRange(minimumValue, maximumValue));

            // Assert
            entries.Should().BeInRange(minimumValue, maximumValue);
        }
    }
}