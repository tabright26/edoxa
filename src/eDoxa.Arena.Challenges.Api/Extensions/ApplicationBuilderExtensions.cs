// Filename: ApplicationBuilderExtensions.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.ServiceBus.Abstractions;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Arena.Challenges.Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseServiceBusSubscriber(this IApplicationBuilder application)
        {
            var serviceBusSubscriber = application.ApplicationServices.GetRequiredService<IServiceBusSubscriber>();
        }
    }
}
