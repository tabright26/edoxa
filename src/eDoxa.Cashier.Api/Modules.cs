// Filename: Modules.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using Autofac;

using eDoxa.Cashier.Api.Application.Abstractions;
using eDoxa.Cashier.Api.Application.Queries;
using eDoxa.Cashier.Domain.Abstractions.Services;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.Domain.Services;
using eDoxa.Cashier.Infrastructure;
using eDoxa.Cashier.Infrastructure.Repositories;
using eDoxa.Commands;
using eDoxa.IntegrationEvents;
using eDoxa.Seedwork.Application.DomainEvents;

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
