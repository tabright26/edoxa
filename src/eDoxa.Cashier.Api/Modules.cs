// Filename: Modules.cs
// Date Created: 2019-05-31
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using Autofac;

using eDoxa.Cashier.Application.Queries;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.DTO.Queries;
using eDoxa.Cashier.Infrastructure;
using eDoxa.Cashier.Infrastructure.Repositories;
using eDoxa.Cashier.Services;
using eDoxa.Cashier.Services.Abstractions;
using eDoxa.Seedwork.Application.Commands;
using eDoxa.Seedwork.Application.DomainEvents;
using eDoxa.ServiceBus;

using JetBrains.Annotations;

namespace eDoxa.Cashier.Api
{
    public sealed class Modules : Module
    {
        protected override void Load([NotNull] ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterModule<DomainEventModule>();

            builder.RegisterModule<CommandModule>();

            builder.RegisterModule<IntegrationEventModule<CashierDbContext>>();

            // Repositories
            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerLifetimeScope();

            builder.RegisterType<AccountRepository>().As<IAccountRepository>().InstancePerLifetimeScope();

            // Services
            builder.RegisterType<AccountService>().As<IAccountService>().InstancePerLifetimeScope();

            // Queries
            builder.RegisterType<CardQuery>().As<ICardQuery>().InstancePerLifetimeScope();

            builder.RegisterType<BalanceQuery>().As<IBalanceQuery>().InstancePerLifetimeScope();

            builder.RegisterType<TransactionQuery>().As<ITransactionQuery>().InstancePerLifetimeScope();
        }
    }
}
