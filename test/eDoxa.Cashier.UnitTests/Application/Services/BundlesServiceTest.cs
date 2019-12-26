// Filename: BundlesServiceTest.cs
// Date Created: 2019-11-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Collections.Immutable;

using eDoxa.Cashier.Api.Application;
using eDoxa.Cashier.Api.Application.Services;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.TestHelper;
using eDoxa.Cashier.TestHelper.Fixtures;

using FluentAssertions;

using Microsoft.Extensions.Options;

using Xunit;

namespace eDoxa.Cashier.UnitTests.Application.Services
{
    public sealed class BundlesServiceTest : UnitTest
    {
        public BundlesServiceTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        [Fact]
        public void FetchDepositMoneyBundles_ShouldBeOfTypeValidationResult()
        {
            // Arrange
            var depositOptions = new HashSet<TransactionBundlesOptions.TransactionBundleOptions>
            {
                new TransactionBundlesOptions.TransactionBundleOptions
                {
                    Amount = 20,
                    Price = 20
                }
            };

            var moneyDepositDictionary = new Dictionary<string, HashSet<TransactionBundlesOptions.TransactionBundleOptions>>
            {
                {"Money", depositOptions}
            };

            var bundlesOptions = new TransactionBundlesOptions
            {
                Deposit = moneyDepositDictionary
            };

            var options = new OptionsWrapper<TransactionBundlesOptions>(bundlesOptions);

            var service = new BundleService(options);

            // Act
            var result = service.FetchDepositMoneyBundles();

            // Assert
            result.Should().BeOfType<ImmutableHashSet<Bundle>>();
        }

        [Fact]
        public void FetchDepositTokenBundles_ShouldBeOfTypeValidationResult()
        {
            // Arrange
            var depositOptions = new HashSet<TransactionBundlesOptions.TransactionBundleOptions>
            {
                new TransactionBundlesOptions.TransactionBundleOptions
                {
                    Amount = 20,
                    Price = 20
                }
            };

            var moneyDepositDictionary = new Dictionary<string, HashSet<TransactionBundlesOptions.TransactionBundleOptions>>
            {
                {"Token", depositOptions}
            };

            var bundlesOptions = new TransactionBundlesOptions
            {
                Deposit = moneyDepositDictionary
            };

            var options = new OptionsWrapper<TransactionBundlesOptions>(bundlesOptions);

            var service = new BundleService(options);

            // Act
            var result = service.FetchDepositTokenBundles();

            // Assert
            result.Should().BeOfType<ImmutableHashSet<Bundle>>();
        }

        [Fact]
        public void FetchWithdrawalMoneyBundles_ShouldBeOfTypeValidationResult()
        {
            // Arrange
            var withdrawalOption = new HashSet<TransactionBundlesOptions.TransactionBundleOptions>
            {
                new TransactionBundlesOptions.TransactionBundleOptions
                {
                    Amount = 20,
                    Price = 20
                }
            };

            var moneyWithdrawalDictionary = new Dictionary<string, HashSet<TransactionBundlesOptions.TransactionBundleOptions>>
            {
                {"Money", withdrawalOption}
            };

            var bundlesOptions = new TransactionBundlesOptions
            {
                Withdrawal = moneyWithdrawalDictionary
            };

            var options = new OptionsWrapper<TransactionBundlesOptions>(bundlesOptions);

            var service = new BundleService(options);

            // Act
            var result = service.FetchWithdrawalMoneyBundles();

            // Assert
            result.Should().BeOfType<ImmutableHashSet<Bundle>>();
        }
    }
}
