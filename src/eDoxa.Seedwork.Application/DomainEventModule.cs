using Autofac;

using JetBrains.Annotations;
using System.Reflection;

using eDoxa.Seedwork.Application.DomainEventHandlers;

using Module = Autofac.Module;

namespace eDoxa.Seedwork.Application
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
