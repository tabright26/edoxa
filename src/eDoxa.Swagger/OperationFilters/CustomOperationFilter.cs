// Filename: GroupNameOperationFilter.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace eDoxa.Swagger.OperationFilters
{
    public class CustomOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            var relativePath = context.ApiDescription.RelativePath;

            var urlSegments = relativePath.Split('/');

            var urlSegment = urlSegments[1];

            if (urlSegment != context.ApiDescription.GroupName)
            {
                var groupName = urlSegment[0].ToString().ToUpper() + urlSegment.Substring(1);

                operation.Tags = new List<string>
                {
                    groupName
                };
            }
        }
    }
}