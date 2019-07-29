// Filename: IRabbitMqServiceBusContext.cs
// Date Created: 2019-07-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using RabbitMQ.Client;

namespace eDoxa.Seedwork.ServiceBus.RabbitMq
{
    public interface IRabbitMqServiceBusContext : IDisposable
    {
        bool IsConnected { get; }

        bool TryConnect();

        IModel CreateChannel();
    }
}
