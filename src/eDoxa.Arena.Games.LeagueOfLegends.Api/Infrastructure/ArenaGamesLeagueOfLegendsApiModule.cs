// Filename: ArenaGamesLeagueOfLegendsApiModule.cs
// Date Created: 2019-10-04
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Autofac;

using eDoxa.Arena.Games.LeagueOfLegends.Api.Areas.Summoner.Services;
using eDoxa.Arena.Games.LeagueOfLegends.Api.Areas.Summoner.Services.Abstractions;

using Microsoft.Extensions.Caching.Distributed;

namespace eDoxa.Arena.Games.LeagueOfLegends.Api.Infrastructure
{
    public class ArenaGamesLeagueOfLegendsApiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Service
            builder.RegisterType<SummonerService>().As<ISummonerService>().InstancePerLifetimeScope();

            // Cache
            builder.RegisterType<MemoryDistributedCache>().As<IDistributedCache>().SingleInstance();
        }
    }
}
