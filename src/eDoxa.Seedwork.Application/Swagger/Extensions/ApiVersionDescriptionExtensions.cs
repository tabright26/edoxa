// Filename: ApiVersionDescriptionExtensions.cs
// Date Created: 2019-08-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Monitoring.AppSettings;

using Microsoft.AspNetCore.Mvc.ApiExplorer;

using Swashbuckle.AspNetCore.Swagger;

namespace eDoxa.Seedwork.Application.Swagger.Extensions
{
    public static class ApiVersionDescriptionExtensions
    {
        public static Info CreateInfoForApiVersion(this ApiVersionDescription description, IHasApiResourceAppSettings appSettings)
        {
            var info = new Info
            {
                Title = appSettings.ApiResource.DisplayName,
                Version = description.GroupName,
                Description = appSettings.ApiResource?.Description,
                Contact = new Contact
                {
                    Name = "Francis Quenneville",
                    Email = "francis@edoxa.gg"
                },
                TermsOfService = "eDoxa"
            };

            if (description.IsDeprecated)
            {
                info.Description += " This API version has been deprecated.";
            }

            return info;
        }
    }
}
