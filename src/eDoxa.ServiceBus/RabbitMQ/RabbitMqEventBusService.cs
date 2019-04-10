// Filename: RabbitMqEventBusService.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

using Autofac;

using Microsoft.Extensions.Logging;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Polly;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;

namespace eDoxa.ServiceBus.RabbitMQ
{
    public class RabbitMqEventBusService : IEventBusService
    {
        private const string Exchange = "edoxa.rabbitmq.broker";
        private const string LifetimeScopeTag = Exchange;

        private readonly int _retryCount;
        private readonly ILogger<RabbitMqEventBusService> _logger;
        private readonly ILifetimeScope _scope;
        private readonly IRabbitMqPersistentConnection _connection;
        private readonly ISubscriptionHandler _handler;

        private string _queue;
        private IModel _channel;

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:eDoxa.EventBus.RabbitMQ.RabbitMqServiceBus" /> class.
        /// </summary>
        /// <param name="logger">The <see cref="T:Microsoft.Extensions.Logging.ILogger" />.</param>
        /// <param name="scope">The <see cref="T:Autofac.ILifetimeScope" />.</param>
        /// <param name="connection">The <see cref="T:eDoxa.EventBus.RabbitMQ.IRabbitMqServiceBusPersistentConnection" />.</param>
        /// <param name="handler">The <see cref="T:eDoxa.EventBus.IEventBusSubscriptionHandler" />.</param>
        /// <param name="retryCount">The retry count of connection attempt.</param>
        /// <param name="queue">The queue name based on the client subscription name.</param>
        public RabbitMqEventBusService(
            ILogger<RabbitMqEventBusService> logger,
            ILifetimeScope scope,
            IRabbitMqPersistentConnection connection,
            ISubscriptionHandler handler,
            int retryCount = 5,
            string queue = null)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _scope = scope ?? throw new ArgumentNullException(nameof(scope));
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            _handler = handler ?? new InMemorySubscriptionHandler();
            _handler.OnIntegrationEventRemoved += this.OnIntegrationEventRemoved;
            _retryCount = retryCount;
            _queue = queue;
            _channel = this.CreateChannel();
        }

        public void Dispose()
        {
            _channel?.Dispose();

            _handler.ClearSubscriptions();
        }

        public void Publish(IntegrationEvent integrationEvent)
        {
            if (!_connection.IsConnected)
            {
                _connection.TryConnect();
            }

            var retryPolicy = Policy.Handle<BrokerUnreachableException>()
                                    .Or<SocketException>()
                                    .WaitAndRetry(
                                        _retryCount,
                                        retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                                        (exception, timeSpan) =>
                                        {
                                            _logger.LogWarning(exception.ToString());
                                        }
                                    );

            using (var channel = _connection.CreateChannel())
            {
                var integrationEventName = integrationEvent.GetType().Name;

                channel.ExchangeDeclare(Exchange, ExchangeType.Direct);

                var jsonString = JsonConvert.SerializeObject(integrationEvent);

                retryPolicy.Execute(
                    () =>
                    {
                        var basicProperties = channel.CreateBasicProperties();

                        basicProperties.DeliveryMode = (int) RabbitMqDeliveryMode.Persistent;

                        channel.BasicPublish(Exchange, integrationEventName, true, basicProperties, Encoding.UTF8.GetBytes(jsonString));
                    }
                );
            }
        }

        public void Subscribe<TIntegrationEvent, TIntegrationEventHandler>()
        where TIntegrationEvent : IntegrationEvent
        where TIntegrationEventHandler : IIntegrationEventHandler<TIntegrationEvent>
        {
            var integrationEventName = _handler.GetIntegrationEventKey<TIntegrationEvent>();

            this.QueueBind(integrationEventName);

            _handler.AddSubscription<TIntegrationEvent, TIntegrationEventHandler>();
        }

        public void SubscribeDynamic<TDynamicIntegrationEventHandler>(string integrationEventName)
        where TDynamicIntegrationEventHandler : IDynamicIntegrationEventHandler
        {
            this.QueueBind(integrationEventName);

            _handler.AddDynamicSubscription<TDynamicIntegrationEventHandler>(integrationEventName);
        }

        public void Unsubscribe<TIntegrationEvent, TIntegrationEventHandler>()
        where TIntegrationEvent : IntegrationEvent
        where TIntegrationEventHandler : IIntegrationEventHandler<TIntegrationEvent>
        {
            _handler.RemoveSubscription<TIntegrationEvent, TIntegrationEventHandler>();
        }

        public void UnsubscribeDynamic<TDynamicIntegrationEventHandler>(string integrationEventName)
        where TDynamicIntegrationEventHandler : IDynamicIntegrationEventHandler
        {
            _handler.RemoveDynamicSubscription<TDynamicIntegrationEventHandler>(integrationEventName);
        }

        /// <summary>
        ///     Raised when the integration event is removed.
        /// </summary>
        /// <param name="sender">The object that invoked the event that fired the event handler.</param>
        /// <param name="integrationEventName">The <see cref="T:eDoxa.EventBus.Events.IntegrationEvent" /> name.</param>
        private void OnIntegrationEventRemoved(object sender, string integrationEventName)
        {
            if (!_connection.IsConnected)
            {
                _connection.TryConnect();
            }

            using (var channel = _connection.CreateChannel())
            {
                channel.QueueUnbind(_queue, Exchange, integrationEventName);

                if (!_handler.IsEmpty)
                {
                    return;
                }

                _queue = string.Empty;

                _channel.Close();
            }
        }

        /// <summary>
        ///     Binds a queue with a specific integration event name.
        /// </summary>
        /// <param name="integrationEventName">The <see cref="T:eDoxa.EventBus.Events.IntegrationEvent" /> name.</param>
        private void QueueBind(string integrationEventName)
        {
            if (_handler.ContainsIntegrationEvent(integrationEventName))
            {
                return;
            }

            if (!_connection.IsConnected)
            {
                _connection.TryConnect();
            }

            using (var channel = _connection.CreateChannel())
            {
                channel.QueueBind(_queue, Exchange, integrationEventName);
            }
        }

        /// <summary>
        ///     Create and return a fresh channel, session, and model.
        /// </summary>
        /// <returns>The event bus channel.</returns>
        private IModel CreateChannel()
        {
            if (!_connection.IsConnected)
            {
                _connection.TryConnect();
            }

            var channel = _connection.CreateChannel();

            channel.ExchangeDeclare(Exchange, ExchangeType.Direct);

            channel.QueueDeclare(_queue, true, false, false, null);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += async (sender, eventArgs) =>
            {
                var integrationEventName = eventArgs.RoutingKey;

                var message = Encoding.UTF8.GetString(eventArgs.Body);

                await this.ProcessIntegrationEventAsync(message, integrationEventName);

                channel.BasicAck(eventArgs.DeliveryTag, false);
            };

            channel.BasicConsume(_queue, false, consumer);

            channel.CallbackException += (sender, eventArgs) =>
            {
                _channel.Dispose();

                _channel = this.CreateChannel();
            };

            return channel;
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