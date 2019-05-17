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

using eDoxa.Security;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace eDoxa.Swagger.Filters
{
    public sealed class CustomOperationFilter : IOperationFilter
    {
        public void Apply([NotNull] Operation operation, [NotNull] OperationFilterContext context)
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

                        //operation.Parameters.Add(
                        //    new NonBodyParameter
                        //    {
                        //        Name = HeaderNames.Authorization,
                        //        In = "header",
                        //        Required = true,
                        //        Type = "string"
                        //    }
                        //);

                        operation.Security = new List<IDictionary<string, IEnumerable<string>>>
                        {
                            new Dictionary<string, IEnumerable<string>>
                            {
                                {
                                    "oauth2", new[]
                                    {
                                        CustomScopes.IdentityApi, CustomScopes.CashierApi, CustomScopes.ChallengeApi
                                    }
                                }
                            }
                        };
                    }
                }
            }

            if (context.ApiDescription.HttpMethod == HttpMethods.Put ||
                context.ApiDescription.HttpMethod == HttpMethods.Post ||
                context.ApiDescription.HttpMethod == HttpMethods.Delete ||
                context.ApiDescription.HttpMethod == HttpMethods.Patch)
            {
                operation.Parameters.Add(
                    new NonBodyParameter
                    {
                        Name = CustomHeaderNames.IdempotencyKey, In = "header", Type = "string", Required = false
                    }
                );
            }
        }
    }
}