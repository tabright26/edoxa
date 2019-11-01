// Filename: LeagueOfLegendsModule.cs
// Date Created: 2019-11-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Autofac;

using eDoxa.Arena.Games.Abstractions.Adapter;
using eDoxa.Arena.Games.LeagueOfLegends.Adapter;

namespace eDoxa.Arena.Games.LeagueOfLegends
{
    public sealed class LeagueOfLegendsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Adapters
            builder.RegisterType<LeagueOfLegendsAuthFactorGeneratorAdapter>().As<IAuthFactorGeneratorAdapter>().InstancePerDependency();
            builder.RegisterType<LeagueOfLegendsAuthFactorValidatorAdapter>().As<IAuthFactorValidatorAdapter>().InstancePerDependency();
        }
    }
}
