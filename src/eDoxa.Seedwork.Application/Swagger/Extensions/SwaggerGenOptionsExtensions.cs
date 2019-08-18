// Filename: SwaggerGenOptionsExtensions.cs
// Date Created: 2019-08-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;

using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Monitoring.AppSettings;

using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;

using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace eDoxa.Seedwork.Application.Swagger.Extensions
{
    internal static class SwaggerGenOptionsExtensions
    {
        public static void SwaggerDocs(this SwaggerGenOptions options, IApiVersionDescriptionProvider provider, IHasApiResourceAppSettings appSettings)
        {
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, description.CreateInfoForApiVersion(appSettings));
            }
        }

        public static void AddSecurityDefinition(this SwaggerGenOptions options, IHasApiResourceAppSettings appSettings)
        {
            options.AddSecurityDefinition(
                "oauth2",
                new OAuth2Scheme
                {
                    Type = "oauth2",
                    Flow = "implicit",
                    AuthorizationUrl = $"{appSettings.Authority.PublicUrl}/connect/authorize",
                    TokenUrl = $"{appSettings.Authority.PublicUrl}/connect/token",
                    Scopes = new Dictionary<string, string>
                    {
                        [appSettings.ApiResource.Name] = appSettings.ApiResource.DisplayName
                    }
                }
            );
        }

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
                    }
                );
            }
        }
    }
}
