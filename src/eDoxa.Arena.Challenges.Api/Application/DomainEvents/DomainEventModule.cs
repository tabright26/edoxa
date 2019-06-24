// Filename: DomainEventModule.cs
// Date Created: 2019-06-24
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using Autofac;

using eDoxa.Arena.Challenges.Api.Application.DomainEvents.Abstractions;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Api.Application.DomainEvents
{
    public sealed class DomainEventModule : Module
    {
        protected override void Load([NotNull] ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies()).AsClosedTypesOf(typeof(IDomainEventHandler<>)).AsImplementedInterfaces();
        }
    }
}
