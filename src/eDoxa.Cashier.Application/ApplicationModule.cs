// Filename: ApplicationModule.cs
// Date Created: 2019-04-09
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
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.DTO.Queries;
using eDoxa.Cashier.Infrastructure;
using eDoxa.Cashier.Infrastructure.Repositories;

using Stripe;

namespace eDoxa.Cashier.Application
{
    public sealed class ApplicationModule : CustomServiceModule<ApplicationModule, CashierDbContext>
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            // Repositories
            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerLifetimeScope();

            // Services            
            builder.RegisterType<CustomerService>().InstancePerLifetimeScope();
            builder.RegisterType<CardService>().InstancePerLifetimeScope();

            // Queries
            builder.RegisterType<AccountQueries>().As<IAccountQueries>().InstancePerLifetimeScope();
            builder.RegisterType<AddressQueries>().As<IAddressQueries>().InstancePerLifetimeScope();
            builder.RegisterType<CardQueries>().As<ICardQueries>().InstancePerLifetimeScope();
        }
    }
}