using System.Reflection;

using Autofac;

using eDoxa.Seedwork.Application.DomainEventHandlers;

using JetBrains.Annotations;

using Module = Autofac.Module;

namespace eDoxa.Seedwork.Application.Modules
{
    public sealed class DomainEventModule<TDomainEventHandler> : Module
    {
        protected override void Load([NotNull] ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterAssemblyTypes(typeof(TDomainEventHandler).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IDomainEventHandler<>))
                .AsImplementedInterfaces();
        }
    }
}
