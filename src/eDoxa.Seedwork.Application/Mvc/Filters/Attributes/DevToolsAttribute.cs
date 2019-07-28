// Filename: DevelopmentOnlyAttribute.cs
// Date Created: 2019-06-13
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Seedwork.Application.Mvc.Filters.Attributes
{
    public sealed class DevToolsAttribute : Attribute, IResourceFilter
    {
        public void OnResourceExecuting( ResourceExecutingContext context)
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

        public void OnResourceExecuted( ResourceExecutedContext context)
        {
        }
    }
}
