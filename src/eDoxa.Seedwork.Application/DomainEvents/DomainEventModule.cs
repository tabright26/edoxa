using System;

using Autofac;

using eDoxa.Seedwork.Application.DomainEvents.Handlers;

using JetBrains.Annotations;

using Module = Autofac.Module;

namespace eDoxa.Seedwork.Application.DomainEvents
{
    public sealed class DomainEventModule : Module
    {
        protected override void Load([NotNull] ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .AsClosedTypesOf(typeof(IDomainEventHandler<>))
                .AsImplementedInterfaces();
        }
    }
}
