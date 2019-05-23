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

using eDoxa.Cashier.Application.Queries;
using eDoxa.Cashier.Domain.Services;
using eDoxa.Cashier.Domain.Services.Abstractions;
using eDoxa.Cashier.DTO.Queries;
using eDoxa.Cashier.Infrastructure;
using eDoxa.Cashier.Infrastructure.Modules;
using eDoxa.Cashier.Security;
using eDoxa.Cashier.Security.Abstractions;
using eDoxa.Commands;
using eDoxa.Seedwork.Application;
using eDoxa.ServiceBus;

using JetBrains.Annotations;

namespace eDoxa.Cashier.Application.Modules
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

            builder.RegisterModule<InfrastructureModule>();

            // Services
            builder.RegisterType<MoneyAccountService>().As<IMoneyAccountService>().InstancePerLifetimeScope();

            builder.RegisterType<TokenAccountService>().As<ITokenAccountService>().InstancePerLifetimeScope();

            // Queries
            builder.RegisterType<StripeCardQueries>().As<IStripeCardQueries>().InstancePerLifetimeScope();

            builder.RegisterType<AccountQueries>().As<IAccountQueries>().InstancePerLifetimeScope();

            builder.RegisterType<TransactionQueries>().As<ITransactionQueries>().InstancePerLifetimeScope();

            // Security
            builder.RegisterType<CashierHttpContext>().As<ICashierHttpContext>().InstancePerLifetimeScope();
        }
    }
}
