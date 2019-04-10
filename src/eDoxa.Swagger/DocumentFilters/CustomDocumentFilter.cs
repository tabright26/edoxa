﻿// Filename: CustomDocumentFilter.cs
// Date Created: 2019-03-17
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;

using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace eDoxa.Swagger.DocumentFilters
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