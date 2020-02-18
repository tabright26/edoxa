// Filename: TestMockFixture.cs
// Date Created: 2020-02-10
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;

using eDoxa.Cashier.Api.Infrastructure;
using eDoxa.Cashier.Domain.Factories;
using eDoxa.Cashier.Domain.Queries;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.Domain.Services;
using eDoxa.Cashier.Domain.Strategies;
using eDoxa.ServiceBus.Abstractions;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

using Moq;

namespace eDoxa.Cashier.TestHelper.Fixtures
{
    public sealed class TestMockFixture
    {
        private static readonly Lazy<CashierAppSettings> _cashierAppSettingsOptions = new Lazy<CashierAppSettings>(
            () =>
            {
                var builder = new ConfigurationBuilder();

                builder.AddJsonFile("appsettings.json", false);

                var configuration = builder.Build();

                return configuration.Get<CashierAppSettings>();
            });

        public TestMockFixture()
        {
            AccountQuery = new Mock<IAccountQuery>();
            ChallengeQuery = new Mock<IChallengeQuery>();
            TransactionQuery = new Mock<ITransactionQuery>();
            AccountRepository = new Mock<IAccountRepository>();
            ChallengeRepository = new Mock<IChallengeRepository>();
            PromotionRepository = new Mock<IPromotionRepository>();
            AccountService = new Mock<IAccountService>();
            ChallengeService = new Mock<IChallengeService>();
            PromotionService = new Mock<IPromotionService>();
            ChallengePayoutStrategy = new Mock<IChallengePayoutStrategy>();
            ChallengePayoutFactory = new Mock<IChallengePayoutFactory>();
            ServiceBusPublisher = new Mock<IServiceBusPublisher>();
        }

        public Mock<IAccountQuery> AccountQuery { get; }

        public Mock<IChallengeQuery> ChallengeQuery { get; }

        public Mock<ITransactionQuery> TransactionQuery { get; }

        public Mock<IAccountRepository> AccountRepository { get; }

        public Mock<IChallengeRepository> ChallengeRepository { get; }

        public Mock<IPromotionRepository> PromotionRepository { get; }

        public Mock<IAccountService> AccountService { get; }

        public Mock<IChallengeService> ChallengeService { get; }

        public Mock<IPromotionService> PromotionService { get; }

        public Mock<IChallengePayoutStrategy> ChallengePayoutStrategy { get; }

        public Mock<IChallengePayoutFactory> ChallengePayoutFactory { get; }

        public Mock<IServiceBusPublisher> ServiceBusPublisher { get; }

        public Mock<IOptionsSnapshot<CashierAppSettings>> CashierAppSettingsOptions
        {
            get
            {
                var mock = new Mock<IOptionsSnapshot<CashierAppSettings>>();

                mock.Setup(snapshot => snapshot.Value).Returns(_cashierAppSettingsOptions.Value);

                return mock;
            }
        }
    }
}
