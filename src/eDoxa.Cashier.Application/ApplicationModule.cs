// Filename: ApplicationModule.cs
// Date Created: 2019-04-15
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using Autofac;

using eDoxa.Cashier.Application.Queries;
using eDoxa.Cashier.Application.Services;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.Domain.Services;
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
            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerLifetimeScope();

            // Services
            builder.RegisterType<MoneyAccountService>().As<IMoneyAccountService>().InstancePerLifetimeScope();

            builder.RegisterType<TokenAccountService>().As<ITokenAccountService>().InstancePerLifetimeScope();

            // Queries
            builder.RegisterType<AddressQueries>().As<IAddressQueries>().InstancePerLifetimeScope();

            builder.RegisterType<CardQueries>().As<ICardQueries>().InstancePerLifetimeScope();

            builder.RegisterType<MoneyAccountQueries>().As<IMoneyAccountQueries>().InstancePerLifetimeScope();

            builder.RegisterType<TokenAccountQueries>().As<ITokenAccountQueries>().InstancePerLifetimeScope();            
        }
    }
}