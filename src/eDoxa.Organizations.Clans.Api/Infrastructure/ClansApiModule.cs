// Filename: ClansApiModule.cs
// Date Created: 2019-09-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Autofac;

using eDoxa.Organizations.Clans.Api.Areas.Clans.Services;
using eDoxa.Organizations.Clans.Domain.Repositories;
using eDoxa.Organizations.Clans.Domain.Services;
using eDoxa.Organizations.Clans.Infrastructure.Repositories;

namespace eDoxa.Organizations.Clans.Api.Infrastructure
{
    public class ClansApiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Repositories
            builder.RegisterType<ClanRepository>().As<IClanRepository>().InstancePerLifetimeScope();
            builder.RegisterType<CandidatureRepository>().As<ICandidatureRepository>().InstancePerLifetimeScope();
            builder.RegisterType<InvitationRepository>().As<IInvitationRepository>().InstancePerLifetimeScope();

            // Services
            builder.RegisterType<ClanService>().As<IClanService>().InstancePerLifetimeScope();
            builder.RegisterType<CandidatureService>().As<ICandidatureService>().InstancePerLifetimeScope();
            builder.RegisterType<InvitationService>().As<IInvitationService>().InstancePerLifetimeScope();
        }
    }
}
