// Filename: CustomDocumentFilter.cs
// Date Created: 2019-05-17
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;

using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace eDoxa.Swagger.Filters
{
    public sealed class CustomDocumentFilter : IDocumentFilter
    {
        public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
        {
            var paths = swaggerDoc.Paths.OrderBy(path => path.Key).ToList();

            swaggerDoc.Paths = paths.ToDictionary(path => path.Key, path => path.Value);
        }
    }
}