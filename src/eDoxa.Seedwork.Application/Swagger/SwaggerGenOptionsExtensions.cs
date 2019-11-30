// Filename: SwaggerGenOptionsExtensions.cs
// Date Created: 2019-09-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;

using eDoxa.Seedwork.Domain;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace eDoxa.Seedwork.Application.Swagger
{
    internal static class SwaggerGenOptionsExtensions
    {
        public static void DescribeAllEnumerationsAsStrings(this SwaggerGenOptions options)
        {
            foreach (var type in Enumeration.GetTypes())
            {
                options.MapType(
                    type,
                    () => new OpenApiSchema
                    {
                        Type = "string",
                        Enum = Enumeration.GetEnumerations(type).Select(x => new OpenApiString(x.Name)).Cast<IOpenApiAny>().ToList()
                    });
            }
        }
    }
}
