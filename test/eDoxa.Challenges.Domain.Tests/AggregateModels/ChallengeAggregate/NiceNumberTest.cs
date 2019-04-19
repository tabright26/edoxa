using System;
using System.Collections.Generic;
using System.Text;

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate.Helpers;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Challenges.Domain.Tests.AggregateModels.ChallengeAggregate
{
    [TestClass]
    public sealed class NiceNumberTest
    {
        [DataRow(1)]
        [DataRow(5)]
        [DataRow(9)]
        [DataRow(20)]
        [DataRow(25)]
        [DataRow(50)]
        [DataRow(75)]
        [DataRow(100)]
        [DataRow(125)]
        [DataRow(150)]
        [DataRow(1500)]
        [DataRow(10000)]
        [DataRow(50000)]
        [DataRow(100000)]
        [DataRow(500000)]
        [DataRow(1000000)]
        [DataRow(5000000)]
        [DataTestMethod]
        public void TestZero_ShouldBeTrue(int value)
        {
            // Act
            var condition = NiceNumber.IsNiceNum(value);

            // Assert
            condition.Should().BeTrue();
        }

        [DataRow(-1)]
        [DataRow(0)]
        [DataRow(1.3)]
        [DataRow(5.3)]
        [DataRow(10.3)]
        [DataRow(11)]
        [DataRow(125.24)]
        [DataRow(499999.9)]
        [DataRow(4999999)]
        [DataTestMethod]
        public void TestZero_ShouldBeFalse(double value)
        {
            // Act
            var condition = NiceNumber.IsNiceNum(value);

            // Assert
            condition.Should().BeFalse();
        }

        [DataRow(0, 0)]
        [DataRow(1, 1)]
        [DataRow(-10, 0)]
        [DataRow(1.3, 1)]
        [DataRow(20, 12)]
        [DataRow(50, 18)]
        [DataRow(100, 28)]
        [DataRow(125, 29)]
        [DataRow(150, 30)]
        [DataRow(200, 32)]
        [DataRow(500, 39)]
        [DataRow(1000, 49)]
        [DataRow(5000, 60)]
        [DataRow(10000, 70)]

        //[DataRow(4999999, 122)] very slow
        //[DataRow(5000000, 123)] very slow
        [DataTestMethod]
        public void TestZero_ShouldBeArrayLength(double value, int length)
        {
            // Act
            var niceNumbers = NiceNumber.NiceNum(value);

            // Assert
            niceNumbers.Count.Should().Be(length);
        }

        [DataRow(28, -1, new int[0])]
        [DataRow(28, 1, new[] { 1 })]
        [DataRow(1, -1, new[] { 20 })]
        [DataRow(0, -1, new[] { 1, 2, 3, 4, 5 })]
        [DataRow(-10, -1, new[] { 1, 2, 3, 4, 5 })]
        [DataRow(1001.28, 1000, new[] { 1, 2, 100, 1000, 10000 })]
        [DataRow(1001.28, 750, new[] { 1, 2, 100, 750, 10000 })]
        [DataRow(1001.28, 500, new[] { 1, 2, 100, 500, 10000 })]
        [DataTestMethod]
        public void TestEmptyList_ShouldBe(double numToRound, int niceNumber, int[] niceNumbers)
        {
            // Act
            var roundNiceNumber = NiceNumber.RoundToNice(numToRound, new List<int>(niceNumbers));

            // Assert
            roundNiceNumber.Should().Be(niceNumber);
        }
    }
}
