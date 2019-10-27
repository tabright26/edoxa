// Filename: LeagueOfLegendsModule.cs
// Date Created: 2019-10-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Autofac;

using eDoxa.Arena.Games.LeagueOfLegends.Api.Services;
using eDoxa.Arena.Games.LeagueOfLegends.Api.Services.Abstractions;

namespace eDoxa.Arena.Games.LeagueOfLegends.Api.Infrastructure
{
    public class LeagueOfLegendsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Service
            builder.RegisterType<LeagueOfLegendsSummonerService>().As<ILeagueOfLegendsSummonerService>().InstancePerLifetimeScope();
        }
    }
}
