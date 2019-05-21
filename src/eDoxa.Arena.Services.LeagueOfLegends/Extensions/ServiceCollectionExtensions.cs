// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-05-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Challenges.Domain.Services.LeagueOfLegends.Api.Abstractions;

using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Challenges.Domain.Services.LeagueOfLegends.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddLeagueOfLegends(this IServiceCollection services)
        {
            services.AddSingleton<IMatchV4Service, MatchV4Service>();
        }
    }
}
