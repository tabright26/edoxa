// Filename: InfrastructureModule.cs
// Date Created: 2019-05-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using Autofac;

using eDoxa.Arena.Challenges.Domain.Repositories;
using eDoxa.Arena.Challenges.Infrastructure.Repositories;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Infrastructure.Modules
{
    public sealed class InfrastructureModule : Module
    {
        protected override void Load([NotNull] ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<ChallengeRepository>().As<IChallengeRepository>().InstancePerLifetimeScope();

            builder.RegisterType<ParticipantRepository>().As<IParticipantRepository>().InstancePerLifetimeScope();

            builder.RegisterType<MatchRepository>().As<IMatchRepository>().InstancePerLifetimeScope();
        }
    }
}
