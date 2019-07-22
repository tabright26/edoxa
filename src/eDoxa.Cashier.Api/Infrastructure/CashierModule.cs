// Filename: ApiModule.cs
// Date Created: 2019-07-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using Autofac;

using eDoxa.Cashier.Api.Application.Factories;
using eDoxa.Cashier.Api.Application.Services;
using eDoxa.Cashier.Api.Application.Strategies;
using eDoxa.Cashier.Api.Infrastructure.Queries;
using eDoxa.Cashier.Domain.Factories;
using eDoxa.Cashier.Domain.Queries;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.Domain.Services;
using eDoxa.Cashier.Domain.Strategies;
using eDoxa.Cashier.Infrastructure;
using eDoxa.Cashier.Infrastructure.Repositories;
using eDoxa.Commands;
using eDoxa.IntegrationEvents;
using eDoxa.Seedwork.Application.DomainEvents;

using JetBrains.Annotations;

namespace eDoxa.Cashier.Api.Infrastructure
{
    public sealed class CashierModule : Module
    {
        protected override void Load([NotNull] ContainerBuilder builder)
        {
            builder.RegisterModule<DomainEventModule>();
            builder.RegisterModule<CommandModule>();
            builder.RegisterModule<IntegrationEventModule<CashierDbContext>>();
        }
    }
}
