// Filename: ApplicationBuilderExtensions.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using Hellang.Middleware.ProblemDetails;

using Microsoft.AspNetCore.Builder;

namespace eDoxa.Seedwork.Application.ProblemDetails.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseCustomProblemDetails(this IApplicationBuilder application)
        {
            return application.Use(
                async (context, next) =>
                {
                    if (context.Request.Path.StartsWithSegments("/api", StringComparison.OrdinalIgnoreCase))
                    {
                        application.UseProblemDetails();
                    }

                    await next();
                });
        }
    }
}
