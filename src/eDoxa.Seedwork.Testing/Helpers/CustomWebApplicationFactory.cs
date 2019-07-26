// Filename: CustomWebApplicationFactory.cs
// Date Created: 2019-07-23
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using Autofac;

using Microsoft.AspNetCore.Mvc.Testing;

namespace eDoxa.Seedwork.Testing.Helpers
{
    public abstract class CustomWebApplicationFactory<TEntryPoint> : WebApplicationFactory<TEntryPoint>
    where TEntryPoint : class
    {
        public abstract void WithContainerBuilder(Action<ContainerBuilder> builder);
    }
}
