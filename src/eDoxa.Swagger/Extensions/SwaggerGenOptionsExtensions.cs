// Filename: SwaggerGenOptionsExtensions.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;

using eDoxa.Seedwork.Domain.Aggregate;
using eDoxa.Seedwork.Domain.Extensions;

using Microsoft.Extensions.DependencyInjection;

using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace eDoxa.Swagger.Extensions
{
    internal static class SwaggerGenOptionsExtensions
    {
        public static void DescribeAllEnumerationsAsStrings(this SwaggerGenOptions options)
        {
            Enumeration.GetTypes()
                .ForEach(
                    type => options.MapType(
                        type,
                        () => new Schema
                        {
                            Type = "string",
                            Enum = Enumeration.GetEnumerations(type).Cast<object>().ToList()
                        }
                    )
                );
        }
    }
}
