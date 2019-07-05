// Filename: ApiResourceExtensions.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using IdentityServer4.Models;

namespace eDoxa.Seedwork.Application.Swagger.Extensions
{
    public static class ApiResourceExtensions
    {
        public static string SwaggerClientId(this ApiResource apiResource)
        {
            return apiResource.Name + ".swagger";
        }

        public static string SwaggerClientName(this ApiResource apiResource)
        {
            return apiResource.DisplayName + " (Swagger UI)";
        }
    }
}
