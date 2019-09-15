// Filename: ApplicationBuilderExtensions.cs
// Date Created: 2019-09-15
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.ServiceBus.Abstractions;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Organizations.Clans.Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseServiceBusSubscriber(this IApplicationBuilder application)
        {
            var serviceBusSubscriber = application.ApplicationServices.GetRequiredService<IServiceBusSubscriber>();
        }
    }
}
