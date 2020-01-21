// Filename: CashierModule.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Autofac;

using eDoxa.Cashier.Api.Application.Factories;
using eDoxa.Cashier.Api.Application.Services;
using eDoxa.Cashier.Api.Application.Strategies;
using eDoxa.Cashier.Api.Infrastructure.Data;
using eDoxa.Cashier.Api.Infrastructure.Queries;
using eDoxa.Cashier.Domain.Factories;
using eDoxa.Cashier.Domain.Queries;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.Domain.Services;
using eDoxa.Cashier.Domain.Strategies;
using eDoxa.Cashier.Infrastructure.Repositories;
using eDoxa.Seedwork.Application.SqlServer.Abstractions;

namespace eDoxa.Cashier.Api.Infrastructure
{
    internal sealed class CashierModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Cleaner
            builder.RegisterType<CashierDbContextCleaner>().As<IDbContextCleaner>().InstancePerLifetimeScope();

            // Seeder
            builder.RegisterType<CashierDbContextSeeder>().As<IDbContextSeeder>().InstancePerLifetimeScope();

            // Repositories
            builder.RegisterType<PromotionRepository>().As<IPromotionRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ChallengeRepository>().As<IChallengeRepository>().InstancePerLifetimeScope();
            builder.RegisterType<AccountRepository>().As<IAccountRepository>().InstancePerLifetimeScope();

            // Queries
            builder.RegisterType<ChallengeQuery>().As<IChallengeQuery>().InstancePerLifetimeScope();
            builder.RegisterType<AccountQuery>().As<IAccountQuery>().InstancePerLifetimeScope();
            builder.RegisterType<TransactionQuery>().As<ITransactionQuery>().InstancePerLifetimeScope();

            // Services
            builder.RegisterType<PromotionService>().As<IPromotionService>().InstancePerLifetimeScope();
            builder.RegisterType<ChallengeService>().As<IChallengeService>().InstancePerLifetimeScope();
            builder.RegisterType<AccountService>().As<IAccountService>().InstancePerLifetimeScope();

            // Strategies
            builder.RegisterType<ChallengePayoutStrategy>().As<IChallengePayoutStrategy>().SingleInstance();

            // Factories
            builder.RegisterType<ChallengePayoutFactory>().As<IChallengePayoutFactory>().SingleInstance();
        }
    }
}
