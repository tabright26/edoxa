// Filename: SwaggerGenOptionsExtensions.cs
// Date Created: 2019-09-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;

using eDoxa.Seedwork.Domain;

using Microsoft.Extensions.DependencyInjection;

using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace eDoxa.Seedwork.Application.Extensions
{
    internal static class SwaggerGenOptionsExtensions
    {
        public static void DescribeAllEnumerationsAsStrings(this SwaggerGenOptions options)
        {
            foreach (var type in Enumeration.GetTypes())
            {
                options.MapType(
                    type,
                    () => new Schema
                    {
                        Type = "string",
                        Enum = Enumeration.GetEnumerations(type).Cast<object>().ToList()
                    });
            }
        }
    }
}
