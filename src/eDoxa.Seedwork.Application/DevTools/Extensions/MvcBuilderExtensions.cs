// Filename: MvcBuilderExtensions.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Application.DevTools.Controllers;

using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Seedwork.Application.DevTools.Extensions
{
    public static class MvcBuilderExtensions
    {
        public static IMvcBuilder AddDevTools(this IMvcBuilder builder)
        {
            return builder.AddApplicationPart(typeof(DevToolsController).Assembly).AddControllersAsServices();
        }
    }
}
