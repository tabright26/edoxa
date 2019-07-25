// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-04-12
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Seedwork.Monitoring.Extensions;
using eDoxa.Web.Spa.Infrastructure;

using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Web.Spa.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddHealthChecks(this IServiceCollection services, WebSpaAppSettings appSettings)
        {
            var healthChecks = services.AddHealthChecks();
            healthChecks.AddAzureKeyVault(appSettings);
            healthChecks.AddIdentityServer(appSettings);
        }
    }
}