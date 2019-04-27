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

using eDoxa.Autofac;
using eDoxa.Cashier.Application.Queries;
using eDoxa.Cashier.Application.Services;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.Domain.Services;
using eDoxa.Cashier.DTO.Queries;
using eDoxa.Cashier.Infrastructure;
using eDoxa.Cashier.Infrastructure.Repositories;

namespace eDoxa.Cashier.Application
{
    public sealed class ApplicationModule : CustomServiceModule<ApplicationModule, CashierDbContext>
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterModule<MediatorModule<ApplicationModule>>();

            // Repositories
            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerLifetimeScope();

            // Services
            builder.RegisterType<MoneyAccountService>().As<IMoneyAccountService>().InstancePerLifetimeScope();
            builder.RegisterType<TokenAccountService>().As<ITokenAccountService>().InstancePerLifetimeScope();

            // Queries
            builder.RegisterType<MoneyAccountQueries>().As<IMoneyAccountQueries>().InstancePerLifetimeScope();
            builder.RegisterType<TokenAccountQueries>().As<ITokenAccountQueries>().InstancePerLifetimeScope();
            builder.RegisterType<AddressQueries>().As<IAddressQueries>().InstancePerLifetimeScope();
            builder.RegisterType<CardQueries>().As<ICardQueries>().InstancePerLifetimeScope();
        }
    }
}