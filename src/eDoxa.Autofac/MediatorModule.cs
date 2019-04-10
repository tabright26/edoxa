// Filename: MediatorModule.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Reflection;

using Autofac;

using eDoxa.Seedwork.Application.Commands.Behaviors;
using eDoxa.Seedwork.Application.Commands.Handlers;
using eDoxa.Seedwork.Application.DomainEventHandlers;

using MediatR;

using Autofac_Module = Autofac.Module;

namespace eDoxa.Autofac
{
    internal sealed class MediatorModule<TStartup> : Autofac_Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();

            var handlerTypes = new[]
            {
                typeof(IDomainEventHandler<>), typeof(ICommandHandler<,>)
            };

            foreach (var handlerType in handlerTypes)
            {
                builder.RegisterAssemblyTypes(typeof(TStartup).GetTypeInfo().Assembly).AsClosedTypesOf(
                    handlerType
                ).AsImplementedInterfaces();
            }

            // The order of generic registration of behaviors is important.
            builder.RegisterGeneric(typeof(CommandLoggingBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(CommandValidationBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(CommandBehavior<,>)).As(typeof(IPipelineBehavior<,>));

            builder.Register<ServiceFactory>(
                context =>
                {
                    var componentContext = context.Resolve<IComponentContext>();

                    return type => componentContext.TryResolve(type, out var instance) ? instance : null;
                }
            );
        }
    }
}