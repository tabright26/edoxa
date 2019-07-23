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

using JetBrains.Annotations;

namespace eDoxa.Cashier.Api.Infrastructure
{
    public sealed class CashierModule : Module
    {
        protected override void Load([NotNull] ContainerBuilder builder)
        {
        }
    }
}
