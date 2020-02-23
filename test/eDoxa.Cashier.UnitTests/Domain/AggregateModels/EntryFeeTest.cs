// Filename: EntryFeeTest.cs
// Date Created: 2020-02-17
//
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.TestHelper;
using eDoxa.Cashier.TestHelper.Fixtures;

using FluentAssertions;

using Xunit;

namespace eDoxa.Cashier.UnitTests.Domain.AggregateModels
{
    public sealed class EntryFeeTest : UnitTest
    {
        public EntryFeeTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator testValidator) : base(testData, testMapper, testValidator)
        {
        }

        // Francis: Comme c'est des constructeur privé, on dirait que c'est impossible d'utilisé les TheoryMember ou InLineData ???
        [Fact]
        public void MoneyEntryFee_ShouldBeEqualToAmount()
        {
            // Arrange
            var moneyEntryFeeFive = MoneyEntryFee.Five;
            var moneyEntryFeeSeventyFive = MoneyEntryFee.SeventyFive;
            var moneyEntryFeeTwentyFive = MoneyEntryFee.TwentyFive;
            var moneyEntryFeeFifty = MoneyEntryFee.Fifty;
            var moneyEntryFeeOneHundred = MoneyEntryFee.OneHundred;
            var moneyEntryFeeTen = MoneyEntryFee.Ten;
            var moneyEntryFeeTwenty = MoneyEntryFee.Twenty;
            var moneyEntryFeeTwoAndHalf = MoneyEntryFee.TwoAndHalf;

            // Assert
            moneyEntryFeeFive.Amount.Should().Be(5);
            moneyEntryFeeSeventyFive.Amount.Should().Be(75);
            moneyEntryFeeTwentyFive.Amount.Should().Be(25);

            moneyEntryFeeFifty.Amount.Should().Be(50);

            moneyEntryFeeOneHundred.Amount.Should().Be(100);

            moneyEntryFeeTen.Amount.Should().Be(10);

            moneyEntryFeeTwenty.Amount.Should().Be(20);

            moneyEntryFeeTwoAndHalf.Amount.Should().Be(2.5M);
        }

        [Fact]
        public void TokenEntryFee_ShouldBeEqualToAmount()
        {
            // Arrange
            var tokenEntryFeeFiftyThousand = TokenEntryFee.FiftyThousand;
            var tokenEntryFeeFiveThousand = TokenEntryFee.FiveThousand;
            var tokenEntryFeeOneHundredThousand = TokenEntryFee.OneHundredThousand;
            var tokenEntryFeeSeventyFiveThousand = TokenEntryFee.SeventyFiveThousand;
            var tokenEntryFeeTenThousand = TokenEntryFee.TenThousand;
            var tokenEntryFeeTwentyFiveThousand = TokenEntryFee.TwentyFiveThousand;
            var tokenEntryFeeTwoThousandFiveHundred = TokenEntryFee.TwoThousandFiveHundred;

            // Assert
            tokenEntryFeeFiftyThousand.Amount.Should().Be(50000);
            tokenEntryFeeFiveThousand.Amount.Should().Be(5000);
            tokenEntryFeeOneHundredThousand.Amount.Should().Be(100000);
            tokenEntryFeeSeventyFiveThousand.Amount.Should().Be(75000);
            tokenEntryFeeTenThousand.Amount.Should().Be(10000);
            tokenEntryFeeTwentyFiveThousand.Amount.Should().Be(25000);
            tokenEntryFeeTwoThousandFiveHundred.Amount.Should().Be(2500);
        }

        [Fact]
        public void GetPayoutBucketPrizeOrDefault_WhenFreeAndTypeIsMoney_ShouldBeMinValue()
        {
            // Arrange
            var freeMoneyEntryFee = new EntryFee(0, CurrencyType.Money);

            // Act
            var bucket = freeMoneyEntryFee.GetPayoutBucketPrizeOrDefault();

            //Assert
            bucket.Type.Should().Be(CurrencyType.Money);
            bucket.Amount.Should().Be(Money.MinValue);
        }

        [Fact]
        public void GetPayoutBucketPrizeOrDefault_WhenFreeAndTypeIsToken_ShouldBeMinValue()
        {
            // Arrange
            var freeTokenEntryFee = new EntryFee(0, CurrencyType.Token);

            // Act
            var bucket = freeTokenEntryFee.GetPayoutBucketPrizeOrDefault();

            //Assert
            bucket.Type.Should().Be(CurrencyType.Token);
            bucket.Amount.Should().Be(Token.MinValue);
        }

        // Francis: Dans ce cas la, je crois que on devrais throw un invalid operation exception non ??
        [Fact]
        public void GetPayoutBucketPrizeOrDefault_WhenFreeAndTypeIsAll_ShouldBeOfTypeAll()
        {
            // Arrange
            var freeTokenEntryFee = new EntryFee(0, CurrencyType.All);

            // Act
            var bucket = freeTokenEntryFee.GetPayoutBucketPrizeOrDefault();

            //Assert
            bucket.Type.Should().Be(CurrencyType.All);
            bucket.Amount.Should().Be(0);
        }
    }
}
