// Filename: CommandModule.cs
// Date Created: 2019-04-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Reflection;

using Autofac;

using eDoxa.Commands.Abstractions.Handlers;
using eDoxa.Commands.Behaviors;

using FluentValidation;

using JetBrains.Annotations;

using MediatR;

using Module = Autofac.Module;

namespace eDoxa.Commands
{
    public sealed class CommandModule<TCommandHandler> : Module
    {
        protected override void Load([NotNull] ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterAssemblyTypes(typeof(TCommandHandler).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(ICommandHandler<,>))
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(typeof(TCommandHandler).GetTypeInfo().Assembly)
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