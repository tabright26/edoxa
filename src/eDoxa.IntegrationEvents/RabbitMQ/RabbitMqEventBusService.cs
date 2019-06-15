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

namespace eDoxa.IntegrationEvents.RabbitMQ
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

        public RabbitMqEventBusService(
            ILogger<RabbitMqEventBusService> logger,
            ILifetimeScope scope,
            IRabbitMqPersistentConnection connection,
            ISubscriptionHandler handler,
            int retryCount = 5,
            string queue = null)
        {
            _logger = logger;
            _scope = scope;
            _connection = connection;
            _handler = handler;
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