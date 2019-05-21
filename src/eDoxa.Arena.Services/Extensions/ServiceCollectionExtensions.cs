// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-05-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Challenges.Domain.Services.LeagueOfLegends.Api.Extensions;

using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Arena.Services.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddArena(this IServiceCollection services)
        {
            services.AddLeagueOfLegends();
        }
    }
}
