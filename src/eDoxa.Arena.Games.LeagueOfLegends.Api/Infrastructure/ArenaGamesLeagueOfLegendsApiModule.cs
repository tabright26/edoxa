// Filename: ArenaGamesLeagueOfLegendsApiModule.cs
// Date Created: 2019-10-04
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Autofac;

using eDoxa.Arena.Games.LeagueOfLegends.Api.Areas.Summoners.Services;
using eDoxa.Arena.Games.LeagueOfLegends.Api.Areas.Summoners.Services.Abstractions;

namespace eDoxa.Arena.Games.LeagueOfLegends.Api.Infrastructure
{
    public class ArenaGamesLeagueOfLegendsApiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Service
            builder.RegisterType<SummonerService>().As<ISummonerService>().InstancePerLifetimeScope();
        }
    }
}
