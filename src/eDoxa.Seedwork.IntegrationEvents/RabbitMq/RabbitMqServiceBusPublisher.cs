// Filename: RabbitMqServiceBusPublisher.cs
// Date Created: 2019-07-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

using Autofac;

using eDoxa.Seedwork.IntegrationEvents.Infrastructure;

using Microsoft.Extensions.Logging;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Polly;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;

namespace eDoxa.Seedwork.IntegrationEvents.RabbitMq
{
    public class RabbitMqServiceBusPublisher : IServiceBusPublisher
    {
        public enum RabbitMqDeliveryMode
        {
            NonPersistent = 1,
            Persistent = 2
        }

        private const string Exchange = "rabbitmq";
        private const string LifetimeScopeTag = Exchange;

        private readonly int _retryCount;
        private readonly ILogger<RabbitMqServiceBusPublisher> _logger;
        private readonly ILifetimeScope _scope;
        private readonly IRabbitMqServiceBusContext _connection;

        private string? _queue;
        private IModel _channel;

        public RabbitMqServiceBusPublisher(
            ILogger<RabbitMqServiceBusPublisher> logger,
            ILifetimeScope scope,
            IRabbitMqServiceBusContext connection,
            IIntegrationEventSubscriptionStore store,
            int retryCount = 5,
            string? queue = null
        )
        {
            _logger = logger;
            _scope = scope;
            _connection = connection;
            Store = store;
            Store.OnIntegrationEventRemoved += this.OnIntegrationEventRemoved;
            _retryCount = retryCount;
            _queue = queue;
            _channel = this.CreateChannel();
        }

        public IIntegrationEventSubscriptionStore Store { get; }

        public void Dispose()
        {
            _channel?.Dispose();

            Store.Clear();
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

                        basicProperties.DeliveryMode = (byte) RabbitMqDeliveryMode.Persistent;

                        channel.BasicPublish(
                            Exchange,
                            integrationEventName,
                            true,
                            basicProperties,
                            Encoding.UTF8.GetBytes(jsonString)
                        );
                    }
                );
            }
        }

        public void Subscribe<TIntegrationEvent, TIntegrationEventHandler>()
        where TIntegrationEvent : IntegrationEvent
        where TIntegrationEventHandler : IIntegrationEventHandler<TIntegrationEvent>
        {
            var integrationEventName = Store.GetIntegrationEventTypeName<TIntegrationEvent>();

            this.QueueBind(integrationEventName);

            Store.AddSubscription<TIntegrationEvent, TIntegrationEventHandler>();
        }

        public void Subscribe<TDynamicIntegrationEventHandler>(string integrationEventTypeName)
        where TDynamicIntegrationEventHandler : IDynamicIntegrationEventHandler
        {
            this.QueueBind(integrationEventTypeName);

            Store.AddSubscription<TDynamicIntegrationEventHandler>(integrationEventTypeName);
        }

        public void Unsubscribe<TIntegrationEvent, TIntegrationEventHandler>()
        where TIntegrationEvent : IntegrationEvent
        where TIntegrationEventHandler : IIntegrationEventHandler<TIntegrationEvent>
        {
            Store.RemoveSubscription<TIntegrationEvent, TIntegrationEventHandler>();
        }

        public void Unsubscribe<TDynamicIntegrationEventHandler>(string integrationEventTypeName)
        where TDynamicIntegrationEventHandler : IDynamicIntegrationEventHandler
        {
            Store.RemoveSubscription<TDynamicIntegrationEventHandler>(integrationEventTypeName);
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

                if (!Store.IsEmpty)
                {
                    return;
                }

                _queue = string.Empty;

                _channel.Close();
            }
        }

        private void QueueBind(string integrationEventName)
        {
            if (Store.Contains(integrationEventName))
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

            channel.QueueDeclare(
                _queue,
                true,
                false,
                false,
                null
            );

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
            using (var scope = _scope.BeginLifetimeScope(LifetimeScopeTag))
            {
                foreach (var subscription in Store.FetchSubscriptions(integrationEventName))
                {
                    if (subscription.IsDynamic)
                    {
                        if (scope.ResolveOptional(subscription.HandlerType) is IDynamicIntegrationEventHandler handler)
                        {
                            await handler.HandleAsync((dynamic)JObject.Parse(jsonString));
                        }
                    }
                    else
                    {
                        var integrationEventType = Store.TryGetIntegrationEventType(integrationEventName);

                        var integrationEvent = JsonConvert.DeserializeObject(jsonString, integrationEventType);

                        var handler = scope.ResolveOptional(subscription.HandlerType);

                        var concreteType = typeof(IIntegrationEventHandler<>).MakeGenericType(integrationEventType);

                        await (Task)concreteType.GetMethod(nameof(IDynamicIntegrationEventHandler.HandleAsync)).Invoke(handler, new[] { integrationEvent });
                    }
                }
            }
        }
    }
}
