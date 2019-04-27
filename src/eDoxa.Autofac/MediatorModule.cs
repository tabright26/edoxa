// Filename: MediatorModule.cs
// Date Created: 2019-04-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;
using System.Reflection;

using Autofac;

using eDoxa.Functional.Extensions;
using eDoxa.Seedwork.Application.Commands.Behaviors;
using eDoxa.Seedwork.Application.Commands.Handlers;
using eDoxa.Seedwork.Application.DomainEventHandlers;

using FluentValidation;

using JetBrains.Annotations;

using MediatR;

using Module = Autofac.Module;

namespace eDoxa.Autofac
{
    public sealed class MediatorModule<TCommandAssembly> : Module
    {
        private readonly IReadOnlyList<Type> _handlerTypes = new List<Type> {typeof(IDomainEventHandler<>), typeof(ICommandHandler<,>)};

        protected override void Load([NotNull] ContainerBuilder builder)
        {
            _handlerTypes.ForEach(handlerType => builder.RegisterAssemblyTypes(typeof(TCommandAssembly).GetTypeInfo().Assembly)
                .AsClosedTypesOf(handlerType)
                .AsImplementedInterfaces());

            builder.RegisterAssemblyTypes(typeof(TCommandAssembly).GetTypeInfo().Assembly)
                .Where(type => type.IsClosedTypeOf(typeof(IValidator<>)))
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();

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