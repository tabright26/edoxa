// Filename: InfrastructureModule.cs
// Date Created: 2019-05-23
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using Autofac;

using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.Infrastructure.Repositories;

using JetBrains.Annotations;

namespace eDoxa.Cashier.Infrastructure.Modules
{
    public sealed class InfrastructureModule : Module
    {
        protected override void Load([NotNull] ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerLifetimeScope();

            builder.RegisterType<MoneyAccountRepository>().As<IMoneyAccountRepository>().InstancePerLifetimeScope();

            builder.RegisterType<TokenAccountRepository>().As<ITokenAccountRepository>().InstancePerLifetimeScope();
        }
    }
}
