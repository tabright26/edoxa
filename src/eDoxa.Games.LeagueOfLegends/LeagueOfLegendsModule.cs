// Filename: LeagueOfLegendsModule.cs
// Date Created: 2019-11-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Autofac;

using eDoxa.Games.Domain.Adapters;
using eDoxa.Games.LeagueOfLegends.Abstactions;
using eDoxa.Games.LeagueOfLegends.Adapter;
using eDoxa.Games.LeagueOfLegends.Services;

namespace eDoxa.Games.LeagueOfLegends
{
    public sealed class LeagueOfLegendsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Services
            builder.RegisterType<LeagueOfLegendsService>().As<ILeagueOfLegendsService>().SingleInstance();

            // Adapters
            builder.RegisterType<LeagueOfLegendsAuthenticationGeneratorAdapter>().As<IAuthenticationGeneratorAdapter>().InstancePerDependency();
            builder.RegisterType<LeagueOfLegendsAuthenticationValidatorAdapter>().As<IAuthenticationValidatorAdapter>().InstancePerDependency();
            builder.RegisterType<LeagueOfLegendsChallengeScoringAdapter>().As<IChallengeScoringAdapter>().InstancePerDependency();
            builder.RegisterType<LeagueOfLegendsChallengeMatchesAdapter>().As<IChallengeMatchesAdapter>().InstancePerDependency();
        }
    }
}
