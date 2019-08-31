// Filename: DomainEventModule.cs
// Date Created: 2019-08-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using Autofac;

namespace eDoxa.Seedwork.Domain
{
    public sealed class DomainEventModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies()).AsClosedTypesOf(typeof(IDomainEventHandler<>)).AsImplementedInterfaces();
        }
    }
}
