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

using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

namespace eDoxa.Seedwork.ServiceBus.AzureServiceBus
{
    public class AzureServiceBusPublisher : ServiceBusPublisher
    {
        private const string TopicName = "edoxa-azure-topic";
        private const string LifetimeScopeTag = "edoxa.azure.broker";
        private const string IntegrationEventSuffix = nameof(IntegrationEvent);

        private readonly ILogger<AzureServiceBusPublisher> _logger;
        private readonly IAzureServiceBusContext _context;
        private readonly IServiceBusStore _serviceBusStore;
        private readonly SubscriptionClient _subscriptionClient;

        public AzureServiceBusPublisher(
            ILogger<AzureServiceBusPublisher> logger,
            ILifetimeScope lifetimeScope,
            IAzureServiceBusContext context,
            IServiceBusStore serviceBusStore,
            string subscriptionName
        ) : base(serviceBusStore, lifetimeScope, LifetimeScopeTag)
        {
            _logger = logger;
            _context = context;
            _serviceBusStore = serviceBusStore;

            _subscriptionClient = new SubscriptionClient(context.ConnectionStringBuilder.GetNamespaceConnectionString(), TopicName, subscriptionName);

            this.RemoveDefaultRule();
            this.RegisterMessageHandler();
        }

        public override void Dispose()
        {
            _serviceBusStore.Clear();
        }

        public override void Publish(IntegrationEvent integrationEvent)
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

        public override void Subscribe<TIntegrationEvent, TDynamicIntegrationEventHandler>()
        {
            var integrationEventName = typeof(TIntegrationEvent).Name.Replace(IntegrationEventSuffix, string.Empty);

            if (!_serviceBusStore.Contains<TIntegrationEvent>())
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

            _serviceBusStore.AddSubscription<TIntegrationEvent, TDynamicIntegrationEventHandler>();
        }

        public override void Subscribe<TDynamicIntegrationEventHandler>(string integrationEventTypeName)
        {
            _serviceBusStore.AddSubscription<TDynamicIntegrationEventHandler>(integrationEventTypeName);
        }

        public override void Unsubscribe<TIntegrationEvent, TDynamicIntegrationEventHandler>()
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

            _serviceBusStore.RemoveSubscription<TIntegrationEvent, TDynamicIntegrationEventHandler>();
        }

        public override void Unsubscribe<TDynamicIntegrationEventHandler>(string integrationEventTypeName)
        {
            _serviceBusStore.RemoveSubscription<TDynamicIntegrationEventHandler>(integrationEventTypeName);
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

                    await this.ProcessAsync(json, integrationEventName);

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

        protected override async Task ProcessAsync(string json, string integrationEventTypeName)
        {
            _logger.LogTrace($"Processing Azure Service Bus event: {integrationEventTypeName}.");

            if (_serviceBusStore.Contains(integrationEventTypeName))
            {
                await base.ProcessAsync(json, integrationEventTypeName);
            }
            else
            {
                _logger.LogWarning($"No subscription for Azure Service Bus event: {integrationEventTypeName}.");
            }
        }
    }
}
