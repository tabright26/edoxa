// Filename: PayoutChartTest.cs
// Date Created: 2019-06-23
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Infrastructure.Data;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.UnitTests.Infrastructure
{
    [TestClass]
    public sealed class PayoutChartTest
    {
        [TestMethod]
        public void PayoutChart_FromCsv_ShouldNotThrow()
        {
            // Arrange
            var payoutChart = new PayoutChart();

            // Act
            var action = new Action(() => payoutChart.GetPayout(PayoutEntries.Fifteen, MoneyEntryFee.Five));

            // Assert
            action.Should().NotThrow();
        }
    }
}
