// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-06-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Arena.Challenges.Api.Temp.LeagueOfLegends.Extensions;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Arena.Challenges.Api.Temp.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddArenaGames(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddLeagueOfLegends(configuration);
        }
    }
}
