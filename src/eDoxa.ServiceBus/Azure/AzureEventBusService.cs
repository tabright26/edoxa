// Filename: AzureEventBusService.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

using Autofac;

using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace eDoxa.ServiceBus.Azure
{
    public class AzureEventBusService : IEventBusService
    {
        private const string TopicName = "edoxa-azure-topic";
        private const string LifetimeScopeTag = "edoxa.azure.broker";
        private const string IntegrationEventSuffix = nameof(IntegrationEvent);

        private readonly ILogger<AzureEventBusService> _logger;
        private readonly ILifetimeScope _scope;
        private readonly IAzurePersistentConnection _connection;
        private readonly ISubscriptionHandler _handler;
        private readonly SubscriptionClient _subscriptionClient;

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:eDoxa.EventBus.AzureServiceBus.AzureEventBusService" /> class.
        /// </summary>
        /// <param name="logger">The <see cref="T:Microsoft.Extensions.Logging.ILogger" />.</param>
        /// <param name="scope">The <see cref="T:Autofac.ILifetimeScope" />.</param>
        /// <param name="connection">The <see cref="T:eDoxa.EventBus.AzureServiceBus.IAzureEventBusPersistentConnection" />.</param>
        /// <param name="handler">The <see cref="T:eDoxa.EventBus.IEventBusSubscriptionHandler" />.</param>
        /// <param name="subscriptionName">The name of the subscription.</param>
        public AzureEventBusService(
            ILogger<AzureEventBusService> logger,
            ILifetimeScope scope,
            IAzurePersistentConnection connection,
            ISubscriptionHandler handler,
            string subscriptionName)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _scope = scope ?? throw new ArgumentNullException(nameof(scope));
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            _handler = handler ?? new InMemorySubscriptionHandler();

            _subscriptionClient = new SubscriptionClient(connection.ConnectionStringBuilder.GetNamespaceConnectionString(), TopicName, subscriptionName);

            this.RemoveDefaultRule();
            this.RegisterMessageHandler();
        }

        public void Dispose()
        {
            _handler.ClearSubscriptions();
        }

        public void Publish(IntegrationEvent integrationEvent)
        {
            var integrationEventName = integrationEvent.GetType().Name.Replace(IntegrationEventSuffix, string.Empty);

            var jsonString = JsonConvert.SerializeObject(integrationEvent);

            var message = new Message
            {
                MessageId = Guid.NewGuid().ToString(), Body = Encoding.UTF8.GetBytes(jsonString), Label = integrationEventName
            };

            var topicClient = _connection.CreateTopicClient();

            topicClient.SendAsync(message).GetAwaiter().GetResult();
        }

        public void Subscribe<TIntegrationEvent, TDynamicIntegrationEventHandler>()
        where TIntegrationEvent : IntegrationEvent
        where TDynamicIntegrationEventHandler : IIntegrationEventHandler<TIntegrationEvent>
        {
            var integrationEventName = typeof(TIntegrationEvent).Name.Replace(IntegrationEventSuffix, string.Empty);

            if (!_handler.ContainsIntegrationEvent<TIntegrationEvent>())
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

            _handler.AddSubscription<TIntegrationEvent, TDynamicIntegrationEventHandler>();
        }

        public void SubscribeDynamic<TDynamicIntegrationEventHandler>(string integrationEventName)
        where TDynamicIntegrationEventHandler : IDynamicIntegrationEventHandler
        {
            _handler.AddDynamicSubscription<TDynamicIntegrationEventHandler>(integrationEventName);
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

            _handler.RemoveSubscription<TIntegrationEvent, TDynamicIntegrationEventHandler>();
        }

        public void UnsubscribeDynamic<TDynamicIntegrationEventHandler>(string integrationEventName)
        where TDynamicIntegrationEventHandler : IDynamicIntegrationEventHandler
        {
            _handler.RemoveDynamicSubscription<TDynamicIntegrationEventHandler>(integrationEventName);
        }

        /// <summary>
        ///     Removes the default rule on the subscription identified by name.
        /// </summary>
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

        /// <summary>
        ///     Receive messages continuously from the entity. Registers a message handler and begins a new thread to receive
        ///     messages.
        /// </summary>
        /// <remarks>
        ///     This subscription client message handler is awaited on every time a new message is received by the receiver.
        /// </remarks>
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
                    MaxConcurrentCalls = 10, AutoComplete = false
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

        /// <summary>
        ///     Process an integration event by his name with his event arguments as json string asynchronously.
        /// </summary>
        /// <param name="jsonString">The integration event arguments as json string.</param>
        /// <param name="integrationEventName">The <see cref="T:eDoxa.EventBus.Events.IntegrationEvent" /> name.</param>
        /// <returns>
        ///     A <see cref="T:System.Threading.Tasks.Task" /> that completes when the integration event has completed
        ///     processing.
        /// </returns>
        private async Task ProcessIntegrationEventAsync(string jsonString, string integrationEventName)
        {
            if (_handler.ContainsIntegrationEvent(integrationEventName))
            {
                using (var scope = _scope.BeginLifetimeScope(LifetimeScopeTag))
                {
                    var subscriptions = _handler.FindAllSubscriptions(integrationEventName);

                    foreach (var subscription in subscriptions)
                    {
                        if (subscription.IsDynamic)
                        {
                            if (scope.ResolveOptional(subscription.IntegrationEventHandlerType) is IDynamicIntegrationEventHandler handler)
                            {
                                await handler.Handle((dynamic) JObject.Parse(jsonString));
                            }
                        }
                        else
                        {
                            var integrationEventType = _handler.GetIntegrationEventType(integrationEventName);

                            var integrationEvent = JsonConvert.DeserializeObject(jsonString, integrationEventType);

                            var handler = scope.ResolveOptional(subscription.IntegrationEventHandlerType);

                            var concreteType = typeof(IIntegrationEventHandler<>).MakeGenericType(integrationEventType);

                            await (Task) concreteType.GetMethod("Handle")
                                                     .Invoke(
                                                         handler,
                                                         new[]
                                                         {
                                                             integrationEvent
                                                         }
                                                     );
                        }
                    }
                }
            }
        }
    }
}