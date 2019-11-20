﻿// Filename: CashierModule.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Autofac;

using eDoxa.Cashier.Api.Areas.Accounts.Services;
using eDoxa.Cashier.Api.Areas.Accounts.Services.Abstractions;
using eDoxa.Cashier.Api.Areas.Challenges.Factories;
using eDoxa.Cashier.Api.Areas.Challenges.Services;
using eDoxa.Cashier.Api.Areas.Challenges.Services.Abstractions;
using eDoxa.Cashier.Api.Areas.Challenges.Strategies;
using eDoxa.Cashier.Api.Areas.Transactions.Services;
using eDoxa.Cashier.Api.Areas.Transactions.Services.Abstractions;
using eDoxa.Cashier.Api.Infrastructure.Queries;
using eDoxa.Cashier.Domain.Factories;
using eDoxa.Cashier.Domain.Queries;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.Domain.Strategies;
using eDoxa.Cashier.Infrastructure.Repositories;

namespace eDoxa.Cashier.Api.Infrastructure
{
    internal sealed class CashierModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Repositories
            builder.RegisterType<ChallengeRepository>().As<IChallengeRepository>().InstancePerLifetimeScope();
            builder.RegisterType<AccountRepository>().As<IAccountRepository>().InstancePerLifetimeScope();
            builder.RegisterType<TransactionRepository>().As<ITransactionRepository>().InstancePerLifetimeScope();

            // Queries
            builder.RegisterType<ChallengeQuery>().As<IChallengeQuery>().InstancePerLifetimeScope();
            builder.RegisterType<AccountQuery>().As<IAccountQuery>().InstancePerLifetimeScope();
            builder.RegisterType<TransactionQuery>().As<ITransactionQuery>().InstancePerLifetimeScope();

            // Services
            builder.RegisterType<ChallengeService>().As<IChallengeService>().InstancePerLifetimeScope();
            builder.RegisterType<AccountService>().As<IAccountService>().InstancePerLifetimeScope();
            builder.RegisterType<BundlesService>().As<IBundlesService>().InstancePerLifetimeScope();
            builder.RegisterType<TransactionService>().As<ITransactionService>().InstancePerLifetimeScope();

            // Strategies
            builder.RegisterType<ChallengePayoutStrategy>().As<IChallengePayoutStrategy>().SingleInstance();

            // Factories
            builder.RegisterType<ChallengePayoutFactory>().As<IChallengePayoutFactory>().SingleInstance();
        }
    }
}