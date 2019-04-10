// Filename: IdempotencyKeyOperationFilter.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

using eDoxa.Seedwork.Infrastructure.Constants;

using Microsoft.AspNetCore.Http;

using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace eDoxa.Swagger.OperationFilters
{
    public sealed class IdempotencyKeyOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (context.ApiDescription.HttpMethod == HttpMethods.Put ||
                context.ApiDescription.HttpMethod == HttpMethods.Post ||
                context.ApiDescription.HttpMethod == HttpMethods.Delete ||
                context.ApiDescription.HttpMethod == HttpMethods.Patch)
            {
                if (operation.Parameters == null)
                {
                    operation.Parameters = new List<IParameter>();
                }

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