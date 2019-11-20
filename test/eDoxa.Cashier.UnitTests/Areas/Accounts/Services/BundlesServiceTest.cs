// Filename: BundlesServiceTest.cs
// Date Created: 2019-11-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Collections.Immutable;

using eDoxa.Cashier.Api.Areas.Accounts;
using eDoxa.Cashier.Api.Areas.Accounts.Services;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.TestHelper;
using eDoxa.Cashier.TestHelper.Fixtures;

using FluentAssertions;

using Microsoft.Extensions.Options;

using Xunit;

namespace eDoxa.Cashier.UnitTests.Areas.Accounts.Services
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
            var depositOptions = new HashSet<BundlesOptions.BundleOptions>
            {
                new BundlesOptions.BundleOptions
                {
                    Amount = 20,
                    Price = 20
                }
            };

            var moneyDepositDictionary = new Dictionary<string, HashSet<BundlesOptions.BundleOptions>>
            {
                {"Money", depositOptions}
            };

            var bundlesOptions = new BundlesOptions
            {
                Deposit = moneyDepositDictionary
            };

            var options = new OptionsWrapper<BundlesOptions>(bundlesOptions);

            var service = new BundlesService(options);

            // Act
            var result = service.FetchDepositMoneyBundles();

            // Assert
            result.Should().BeOfType<ImmutableHashSet<Bundle>>();
        }

        [Fact]
        public void FetchDepositTokenBundles_ShouldBeOfTypeValidationResult()
        {
            // Arrange
            var depositOptions = new HashSet<BundlesOptions.BundleOptions>
            {
                new BundlesOptions.BundleOptions
                {
                    Amount = 20,
                    Price = 20
                }
            };

            var moneyDepositDictionary = new Dictionary<string, HashSet<BundlesOptions.BundleOptions>>
            {
                {"Token", depositOptions}
            };

            var bundlesOptions = new BundlesOptions
            {
                Deposit = moneyDepositDictionary
            };

            var options = new OptionsWrapper<BundlesOptions>(bundlesOptions);

            var service = new BundlesService(options);

            // Act
            var result = service.FetchDepositTokenBundles();

            // Assert
            result.Should().BeOfType<ImmutableHashSet<Bundle>>();
        }

        [Fact]
        public void FetchWithdrawalMoneyBundles_ShouldBeOfTypeValidationResult()
        {
            // Arrange
            var withdrawalOption = new HashSet<BundlesOptions.BundleOptions>
            {
                new BundlesOptions.BundleOptions
                {
                    Amount = 20,
                    Price = 20
                }
            };

            var moneyWithdrawalDictionary = new Dictionary<string, HashSet<BundlesOptions.BundleOptions>>
            {
                {"Money", withdrawalOption}
            };

            var bundlesOptions = new BundlesOptions
            {
                Withdrawal = moneyWithdrawalDictionary
            };

            var options = new OptionsWrapper<BundlesOptions>(bundlesOptions);

            var service = new BundlesService(options);

            // Act
            var result = service.FetchWithdrawalMoneyBundles();

            // Assert
            result.Should().BeOfType<ImmutableHashSet<Bundle>>();
        }
    }
}
