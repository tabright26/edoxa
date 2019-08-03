// Filename: IRequestModule.cs
// Date Created: 2019-06-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Reflection;

using Autofac;

using eDoxa.Seedwork.Application.Behaviors;

using FluentValidation;

using MediatR;

using Module = Autofac.Module;

namespace eDoxa.Seedwork.Application.Modules
{
    public sealed class RequestModule : Module
    {
        protected override void Load( ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies()).AsClosedTypesOf(typeof(IRequestHandler<>)).AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .Where(type => type.IsClosedTypeOf(typeof(IValidator<>)))
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();

            builder.RegisterGeneric(typeof(RequestLoggingBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(RequestValidationBehavior<,>)).As(typeof(IPipelineBehavior<,>));

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
