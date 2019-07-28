// Filename: ApiModule.cs
// Date Created: 2019-07-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using Autofac;

using eDoxa.Seedwork.IntegrationEvents;

namespace eDoxa.Payment.Api.Infrastructure
{
    internal sealed class PaymentApiModule : Module
    {
        protected override void Load( ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies()).AsClosedTypesOf(typeof(IIntegrationEventHandler<>));
        }
    }
}
