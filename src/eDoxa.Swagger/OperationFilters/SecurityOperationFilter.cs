// Filename: SecurityOperationFilter.cs
// Date Created: 2019-04-13
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace eDoxa.Swagger.OperationFilters
{
    public sealed class SecurityOperationFilter : IOperationFilter
    {
        private readonly IConfiguration _configuration;

        public SecurityOperationFilter(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (context.ApiDescription.TryGetMethodInfo(out var methodInfo))
            {
                var authorize = methodInfo.DeclaringType.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any();
                var allowAnonymous = methodInfo.DeclaringType.GetCustomAttributes(true).OfType<AllowAnonymousAttribute>().Any();

                if (authorize && !allowAnonymous)
                {
                    if (operation.Responses == null)
                    {
                        operation.Responses = new Dictionary<string, Response>();
                    }

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
                                    _configuration["IdentityServer:ApiResource:Name"]
                                }
                            }
                        }
                    };
                }
            }
        }
    }
}