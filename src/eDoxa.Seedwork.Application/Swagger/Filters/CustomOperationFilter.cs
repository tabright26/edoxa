// Filename: CustomOperationFilter.cs
// Date Created: 2019-05-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Linq;
using System.Net;

using eDoxa.Seedwork.Security.Constants;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace eDoxa.Seedwork.Application.Swagger.Filters
{
    public sealed class CustomOperationFilter : IOperationFilter
    {
        public void Apply( Operation operation,  OperationFilterContext context)
        {
            ApplySecurityOperation(operation, context);
        }

        private static void ApplySecurityOperation(Operation operation, OperationFilterContext context)
        {
            if (context.ApiDescription.TryGetMethodInfo(out var methodInfo))
            {
                if (methodInfo.DeclaringType != null)
                {
                    var authorize = methodInfo.DeclaringType.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any();
                    var allowAnonymous = methodInfo.DeclaringType.GetCustomAttributes(true).OfType<AllowAnonymousAttribute>().Any();

                    if (authorize && !allowAnonymous)
                    {
                        operation.Responses.Add(
                            StatusCodes.Status401Unauthorized.ToString(),
                            new Response
                            {
                                Description = HttpStatusCode.Unauthorized.ToString()
                            }
                        );

                        operation.Responses.Add(
                            StatusCodes.Status403Forbidden.ToString(),
                            new Response
                            {
                                Description = HttpStatusCode.Forbidden.ToString()
                            }
                        );

                        operation.Security = new List<IDictionary<string, IEnumerable<string>>>
                        {
                            new Dictionary<string, IEnumerable<string>>
                            {
                                {
                                    "oauth2", new[]
                                    {
                                        CustomScopes.IdentityApi, CustomScopes.CashierApi, CustomScopes.ArenaChallengesApi
                                    }
                                }
                            }
                        };
                    }
                }
            }
        }
    }
}