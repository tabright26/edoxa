// Filename: CustomActionFilter.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Globalization;

using eDoxa.Seedwork.Infrastructure.Constants;

using Microsoft.AspNetCore.Mvc.Filters;

namespace eDoxa.Seedwork.Application.Filters
{
    public class CustomActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            context.HttpContext.Response.Headers.Add(CustomHeaderNames.RequestId, Guid.NewGuid().ToString());
            context.HttpContext.Response.Headers.Add(CustomHeaderNames.RequestDate, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture));
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}