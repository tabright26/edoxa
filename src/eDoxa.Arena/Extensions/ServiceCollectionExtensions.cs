// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-06-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Arena.Services.LeagueOfLegends;
using eDoxa.Arena.Services.LeagueOfLegends.Abstractions;

using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Arena.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddArena(this IServiceCollection services)
        {
            services.AddSingleton<ILeagueOfLegendsService, FakeLeagueOfLegendsService>();
        }
    }
}
