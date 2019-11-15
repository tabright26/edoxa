// Filename: AccountDepositPostRequestTest.cs
// Date Created: 2019-09-16
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.Areas.Accounts;
using eDoxa.Cashier.Api.Areas.Accounts.Requests;
using eDoxa.Cashier.Api.Areas.Accounts.Services;
using eDoxa.Cashier.Api.Areas.Accounts.Services.Abstractions;
using eDoxa.Cashier.Api.IntegrationEvents;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.TestHelper;
using eDoxa.Cashier.TestHelper.Fixtures;
using eDoxa.Seedwork.Application.Validations.Extensions;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Miscs;
using eDoxa.ServiceBus.Abstractions;

using FluentAssertions;

using FluentValidation.Results;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using Moq;

using Newtonsoft.Json;

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
                new BundlesOptions.BundleOptions()
                {
                    Amount = 20,
                    Price = 20
                }
            };


            var moneyDepositDictionary = new Dictionary<string, HashSet<BundlesOptions.BundleOptions>>
            {
                {"Money", depositOptions}
            };

            var bundlesOptions = new BundlesOptions(){ Deposit = moneyDepositDictionary};

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
                new BundlesOptions.BundleOptions()
                {
                    Amount = 20,
                    Price = 20
                }
            };

            var moneyDepositDictionary = new Dictionary<string, HashSet<BundlesOptions.BundleOptions>>
            {
                {"Token", depositOptions}
            };

            var bundlesOptions = new BundlesOptions(){ Deposit = moneyDepositDictionary};

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
                new BundlesOptions.BundleOptions()
                {
                    Amount = 20,
                    Price = 20
                }
            };


            var moneyWithdrawalDictionary = new Dictionary<string, HashSet<BundlesOptions.BundleOptions>>
            {
                {"Money", withdrawalOption}
            };

            var bundlesOptions = new BundlesOptions(){ Withdrawal = moneyWithdrawalDictionary};

            var options = new OptionsWrapper<BundlesOptions>(bundlesOptions);

            var service = new BundlesService(options);

            // Act
            var result = service.FetchWithdrawalMoneyBundles();

            // Assert
            result.Should().BeOfType<ImmutableHashSet<Bundle>>();
        }
    }
}
