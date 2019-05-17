// Filename: ApplicationModule.cs
// Date Created: 2019-05-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using Autofac;

using eDoxa.Cashier.Application.Abstractions;
using eDoxa.Cashier.Application.Queries;
using eDoxa.Cashier.Application.Security;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.Domain.Services;
using eDoxa.Cashier.Domain.Services.Abstractions;
using eDoxa.Cashier.DTO.Queries;
using eDoxa.Cashier.Infrastructure;
using eDoxa.Cashier.Infrastructure.Repositories;
using eDoxa.Commands;
using eDoxa.Seedwork.Application;
using eDoxa.ServiceBus;

using JetBrains.Annotations;

namespace eDoxa.Cashier.Application
{
    public sealed class ApplicationModule : Module
    {
        protected override void Load([NotNull] ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterModule<DomainEventModule<ApplicationModule>>();

            builder.RegisterModule<CommandModule<ApplicationModule>>();

            builder.RegisterModule<CommandInfrastructureModule<CashierDbContext>>();

            builder.RegisterModule<IntegrationEventModule<ApplicationModule, CashierDbContext>>();

            // Repositories
            builder.RegisterType<MoneyAccountRepository>().As<IMoneyAccountRepository>().InstancePerLifetimeScope();

            builder.RegisterType<TokenAccountRepository>().As<ITokenAccountRepository>().InstancePerLifetimeScope();

            // Services
            builder.RegisterType<MoneyAccountService>().As<IMoneyAccountService>().InstancePerLifetimeScope();

            builder.RegisterType<TokenAccountService>().As<ITokenAccountService>().InstancePerLifetimeScope();

            // Queries
            builder.RegisterType<StripeCardQueries>().As<IStripeCardQueries>().InstancePerLifetimeScope();

            builder.RegisterType<AccountQueries>().As<IAccountQueries>().InstancePerLifetimeScope();

            builder.RegisterType<TransactionQueries>().As<ITransactionQueries>().InstancePerLifetimeScope();

            // Security
            builder.RegisterType<CashierSecurity>().As<ICashierSecurity>().SingleInstance();
        }
    }
}