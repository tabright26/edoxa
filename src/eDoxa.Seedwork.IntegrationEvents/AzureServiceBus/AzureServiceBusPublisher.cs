// Filename: AzureServiceBusPublisher.cs
// Date Created: 2019-07-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

using Autofac;

using eDoxa.Seedwork.IntegrationEvents.Infrastructure;

using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace eDoxa.Seedwork.IntegrationEvents.AzureServiceBus
{
    public class AzureServiceBusPublisher : IServiceBusPublisher
    {
        private const string TopicName = "edoxa-azure-topic";
        private const string LifetimeScopeTag = "edoxa.azure.broker";
        private const string IntegrationEventSuffix = nameof(IntegrationEvent);

        private readonly ILogger<AzureServiceBusPublisher> _logger;
        private readonly ILifetimeScope _scope;
        private readonly IAzureServiceBusContext _context;
        private readonly IIntegrationEventSubscriptionStore _store;
        private readonly SubscriptionClient _subscriptionClient;

        public AzureServiceBusPublisher(
            ILogger<AzureServiceBusPublisher> logger,
            ILifetimeScope scope,
            IAzureServiceBusContext context,
            IIntegrationEventSubscriptionStore store,
            string subscriptionName
        )
        {
            _logger = logger;
            _scope = scope;
            _context = context;
            _store = store;

            _subscriptionClient = new SubscriptionClient(context.ConnectionStringBuilder.GetNamespaceConnectionString(), TopicName, subscriptionName);

            this.RemoveDefaultRule();
            this.RegisterMessageHandler();
        }

        public void Dispose()
        {
            _store.Clear();
        }

        public void Publish(IntegrationEvent integrationEvent)
        {
            var integrationEventName = integrationEvent.GetType().Name.Replace(IntegrationEventSuffix, string.Empty);

            var jsonString = JsonConvert.SerializeObject(integrationEvent);

            var message = new Message
            {
                MessageId = Guid.NewGuid().ToString(),
                Body = Encoding.UTF8.GetBytes(jsonString),
                Label = integrationEventName
            };

            var topicClient = _context.CreateTopicClient();

            topicClient.SendAsync(message).GetAwaiter().GetResult();
        }

        public void Subscribe<TIntegrationEvent, TDynamicIntegrationEventHandler>()
        where TIntegrationEvent : IntegrationEvent
        where TDynamicIntegrationEventHandler : IIntegrationEventHandler<TIntegrationEvent>
        {
            var integrationEventName = typeof(TIntegrationEvent).Name.Replace(IntegrationEventSuffix, string.Empty);

            if (!_store.Contains<TIntegrationEvent>())
            {
                try
                {
                    _subscriptionClient.AddRuleAsync(
                            new RuleDescription
                            {
                                Filter = new CorrelationFilter
                                {
                                    Label = integrationEventName
                                },
                                Name = integrationEventName
                            }
                        )
                        .GetAwaiter()
                        .GetResult();
                }
                catch (ServiceBusException)
                {
                    _logger.LogInformation($"The messaging entity {integrationEventName} already exists.");
                }
            }

            _store.AddSubscription<TIntegrationEvent, TDynamicIntegrationEventHandler>();
        }

        public void Subscribe<TDynamicIntegrationEventHandler>(string integrationEventTypeName)
        where TDynamicIntegrationEventHandler : IDynamicIntegrationEventHandler
        {
            _store.AddSubscription<TDynamicIntegrationEventHandler>(integrationEventTypeName);
        }

        public void Unsubscribe<TIntegrationEvent, TDynamicIntegrationEventHandler>()
        where TIntegrationEvent : IntegrationEvent
        where TDynamicIntegrationEventHandler : IIntegrationEventHandler<TIntegrationEvent>
        {
            var integrationEventName = typeof(TIntegrationEvent).Name.Replace(IntegrationEventSuffix, string.Empty);

            try
            {
                _subscriptionClient.RemoveRuleAsync(integrationEventName).GetAwaiter().GetResult();
            }
            catch (MessagingEntityNotFoundException)
            {
                _logger.LogInformation($"The messaging entity {integrationEventName} could not be found.");
            }

            _store.RemoveSubscription<TIntegrationEvent, TDynamicIntegrationEventHandler>();
        }

        public void Unsubscribe<TDynamicIntegrationEventHandler>(string integrationEventTypeName)
        where TDynamicIntegrationEventHandler : IDynamicIntegrationEventHandler
        {
            _store.RemoveSubscription<TDynamicIntegrationEventHandler>(integrationEventTypeName);
        }

        private void RemoveDefaultRule()
        {
            try
            {
                _subscriptionClient.RemoveRuleAsync(RuleDescription.DefaultRuleName).GetAwaiter().GetResult();
            }
            catch (MessagingEntityNotFoundException)
            {
                _logger.LogInformation($"The messaging entity {RuleDescription.DefaultRuleName} could not be found.");
            }
        }

        private void RegisterMessageHandler()
        {
            _subscriptionClient.RegisterMessageHandler(
                async (message, token) =>
                {
                    var integrationEventName = $"{message.Label}{IntegrationEventSuffix}";

                    var json = Encoding.UTF8.GetString(message.Body);

                    await this.ProcessIntegrationEventAsync(json, integrationEventName);

                    // Complete the message so that it is not received again.
                    await _subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);
                },
                new MessageHandlerOptions(ExceptionReceivedHandler)
                {
                    MaxConcurrentCalls = 10,
                    AutoComplete = false
                }
            );

            Task ExceptionReceivedHandler(ExceptionReceivedEventArgs eventArgs)
            {
                var context = eventArgs.ExceptionReceivedContext;

                Debug.WriteLine($"Message handler encountered an exception {eventArgs.Exception}.");

                Debug.WriteLine("Exception context for troubleshooting:");

                Debug.WriteLine($"- Endpoint: {context.Endpoint};");

                Debug.WriteLine($"- Entity Path: {context.EntityPath};");

                Debug.WriteLine($"- Executing Action: {context.Action}.");

                return Task.CompletedTask;
            }
        }

        private async Task ProcessIntegrationEventAsync(string jsonString, string integrationEventName)
        {
            if (_store.Contains(integrationEventName))
            {
                using (var scope = _scope.BeginLifetimeScope(LifetimeScopeTag))
                {
                    var subscriptions = _store.FetchSubscriptions(integrationEventName);

                    foreach (var subscription in subscriptions)
                    {
                        if (subscription.IsDynamic)
                        {
                            if (scope.ResolveOptional(subscription.HandlerType) is IDynamicIntegrationEventHandler handler)
                            {
                                await handler.HandleAsync((dynamic) JObject.Parse(jsonString));
                            }
                        }
                        else
                        {
                            var integrationEventType = _store.TryGetIntegrationEventType(integrationEventName);

                            var integrationEvent = JsonConvert.DeserializeObject(jsonString, integrationEventType);

                            var handler = scope.ResolveOptional(subscription.HandlerType);

                            var concreteType = typeof(IIntegrationEventHandler<>).MakeGenericType(integrationEventType);

                            await (Task) concreteType.GetMethod(nameof(IDynamicIntegrationEventHandler.HandleAsync)).Invoke(handler, new[] {integrationEvent});
                        }
                    }
                }
            }
        }
    }
}
