// Filename: FluentValidationModule.cs
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

using FluentValidation;

using Autofac_Module = Autofac.Module;
using Autofac_TypeExtensions = Autofac.TypeExtensions;

namespace eDoxa.Autofac
{
    internal sealed class FluentValidationModule<TStartup> : Autofac_Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(TStartup).GetTypeInfo().Assembly).Where(
                type => type.IsClosedTypeOf(typeof(IValidator<>))
            ).AsImplementedInterfaces();
        }
    }
}