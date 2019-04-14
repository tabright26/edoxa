// Filename: CustomOperationFilter.cs
// Date Created: 2019-04-14
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Linq;
using System.Net;

using eDoxa.IdentityServer;
using eDoxa.Seedwork.Infrastructure.Constants;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;

using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace eDoxa.Swagger.Filters
{
    public sealed class CustomOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            operation.Parameters = new List<IParameter>();

            operation.Responses = new Dictionary<string, Response>();

            operation.Security = new List<IDictionary<string, IEnumerable<string>>>();

            operation.Tags = new List<string>();

            ApplyGroupNameOperation(operation, context);

            ApplySecurityOperation(operation, context);

            ApplyIdempotencyKeyOperation(operation, context);
        }

        private static void ApplyGroupNameOperation(Operation operation, OperationFilterContext context)
        {
            var relativePath = context.ApiDescription.RelativePath;

            var urlSegments = relativePath.Split('/');

            var urlSegment = urlSegments[1];

            if (urlSegment != context.ApiDescription.GroupName)
            {
                var groupName = urlSegment[0].ToString().ToUpper() + urlSegment.Substring(1);

                operation.Tags.Add(groupName);
            }
        }

        private static void ApplySecurityOperation(Operation operation, OperationFilterContext context)
        {
            if (context.ApiDescription.TryGetMethodInfo(out var methodInfo))
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

                    operation.Security.Add(
                        new Dictionary<string, IEnumerable<string>>
                        {
                            {
                                "oauth2", new[]
                                {
                                    CustomScopes.IdentityApi, CustomScopes.CashierApi, CustomScopes.ChallengesApi, CustomScopes.NotificationsApi
                                }
                            }
                        }
                    );
                }
            }
        }

        private static void ApplyIdempotencyKeyOperation(Operation operation, OperationFilterContext context)
        {
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

        private static void ApplyAuthorizationOperation(Operation operation, OperationFilterContext context)
        {
            if (context.ApiDescription.TryGetMethodInfo(out var methodInfo))
            {
                var authorize = methodInfo.DeclaringType.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any();
                var allowAnonymous = methodInfo.DeclaringType.GetCustomAttributes(true).OfType<AllowAnonymousAttribute>().Any();

                if (authorize && !allowAnonymous)
                {
                    operation.Parameters.Add(
                        new NonBodyParameter
                        {
                            Name = HeaderNames.Authorization, In = "header", Required = true, Type = "string"
                        }
                    );
                }
            }
        }
    }
}