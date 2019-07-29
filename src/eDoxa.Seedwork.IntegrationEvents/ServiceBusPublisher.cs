// Filename: ServiceBusPublisher.cs
// Date Created: 2019-07-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Threading.Tasks;

using Autofac;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace eDoxa.Seedwork.IntegrationEvents
{
    public abstract class ServiceBusPublisher : IServiceBusPublisher
    {
        private readonly ILifetimeScope _lifetimeScope;

        protected ServiceBusPublisher(IServiceBusStore serviceBusStore, ILifetimeScope lifetimeScope, string lifetimeScopeTag)
        {
            _lifetimeScope = lifetimeScope;
            Store = serviceBusStore;
            Tag = lifetimeScopeTag;
        }

        protected IServiceBusStore Store { get; }

        private string Tag { get; }

        public abstract void Dispose();

        public abstract void Publish(IntegrationEvent integrationEvent);

        public abstract void Subscribe<TIntegrationEvent, TIntegrationEventHandler>()
        where TIntegrationEvent : IntegrationEvent
        where TIntegrationEventHandler : IIntegrationEventHandler<TIntegrationEvent>;

        public abstract void Subscribe<TDynamicIntegrationEventHandler>(string integrationEventTypeName)
        where TDynamicIntegrationEventHandler : IDynamicIntegrationEventHandler;

        public abstract void Unsubscribe<TIntegrationEvent, TIntegrationEventHandler>()
        where TIntegrationEvent : IntegrationEvent
        where TIntegrationEventHandler : IIntegrationEventHandler<TIntegrationEvent>;

        public abstract void Unsubscribe<TDynamicIntegrationEventHandler>(string integrationEventTypeName)
        where TDynamicIntegrationEventHandler : IDynamicIntegrationEventHandler;

        private static async Task ProcessAsync(IIntegrationEventHandler handler, string json, Type integrationEventType)
        {
            var integrationEvent = JsonConvert.DeserializeObject(json, integrationEventType);

            var genericType = typeof(IIntegrationEventHandler<>).MakeGenericType(integrationEventType);

            await Task.Yield();

            await (Task) genericType.GetMethod(nameof(IDynamicIntegrationEventHandler.HandleAsync)).Invoke(handler, new[] {integrationEvent});
        }

        private static async Task ProcessAsync(IDynamicIntegrationEventHandler handler, string json)
        {
            dynamic integrationEvent = JObject.Parse(json);

            await Task.Yield();

            await handler.HandleAsync(integrationEvent);
        }

        protected virtual async Task ProcessAsync(string json, string integrationEventTypeName)
        {
            using var scope = _lifetimeScope.BeginLifetimeScope(Tag);

            foreach (var handlerType in Store.GetHandlerTypesFor(integrationEventTypeName))
            {
                var integrationEventHandler = scope.ResolveOptional(handlerType);

                switch (integrationEventHandler)
                {
                    case IDynamicIntegrationEventHandler handler:
                    {
                        await ProcessAsync(handler, json);

                        break;
                    }

                    case IIntegrationEventHandler handler:
                    {
                        if (Store.TryGetTypeFor(integrationEventTypeName, out var integrationEventType))
                        {
                            await ProcessAsync(handler, json, integrationEventType);
                        }

                        break;
                    }
                }
            }
        }
    }
}
