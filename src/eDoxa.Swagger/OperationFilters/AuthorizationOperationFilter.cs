// Filename: AuthorizationOperationFilter.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Authorization;
using Microsoft.Net.Http.Headers;

using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace eDoxa.Swagger.OperationFilters
{
    public sealed class AuthorizationOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (context.ApiDescription.TryGetMethodInfo(out var methodInfo))
            {
                var authorize = methodInfo.DeclaringType.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any();
                var allowAnonymous = methodInfo.DeclaringType.GetCustomAttributes(true).OfType<AllowAnonymousAttribute>().Any();

                if (authorize && !allowAnonymous)
                {
                    if (operation.Parameters == null)
                    {
                        operation.Parameters = new List<IParameter>();
                    }

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