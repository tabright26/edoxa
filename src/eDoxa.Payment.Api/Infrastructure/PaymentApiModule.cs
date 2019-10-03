// Filename: PaymentApiModule.cs
// Date Created: 2019-09-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using Autofac;

using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Payment.Api.Infrastructure
{
    internal sealed class PaymentApiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // TODO: Update with ServiceBus package.
            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies()).AsClosedTypesOf(typeof(IIntegrationEventHandler<>));
        }
    }
}
