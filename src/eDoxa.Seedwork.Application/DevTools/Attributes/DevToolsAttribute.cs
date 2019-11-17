// Filename: DevToolsAttribute.cs
// Date Created: 2019-10-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Seedwork.Application.DevTools.Attributes
{
    // TODO: This attribute must be replaced by UseEnpoints in .Net Core 3.0.
    internal sealed class DevToolsAttribute : Attribute, IResourceFilter
    {
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            var environment = context.HttpContext.RequestServices.GetService<IHostingEnvironment>();

            if (environment.IsProduction())
            {
                context.Result = new ContentResult
                {
                    Content = "This request is not allowed in production.",
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status403Forbidden
                };
            }
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
        }
    }
}
