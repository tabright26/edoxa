// Filename: LeagueOfLegendsModule.cs
// Date Created: 2019-10-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Autofac;

using eDoxa.Arena.Games.LeagueOfLegends.Api.Areas.Summoners.Caches;
using eDoxa.Arena.Games.LeagueOfLegends.Api.Areas.Summoners.Services;
using eDoxa.Arena.Games.LeagueOfLegends.Api.Areas.Summoners.Services.Abstractions;

namespace eDoxa.Arena.Games.LeagueOfLegends.Api.Infrastructure
{
    public class LeagueOfLegendsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Service
            builder.RegisterType<LeagueOfLegendsSummonerService>().As<ILeagueOfLegendsSummonerService>().InstancePerLifetimeScope();

            // Caches
            builder.RegisterType<LeagueOfLegendsCache>().As<ILeagueOfLegendsCache>().SingleInstance();
        }
    }
}
