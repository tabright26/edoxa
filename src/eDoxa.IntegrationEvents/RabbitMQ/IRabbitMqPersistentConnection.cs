// Filename: IRabbitMqPersistentConnection.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using RabbitMQ.Client;

namespace eDoxa.IntegrationEvents.RabbitMQ
{
    /// <summary>
    ///     Create a persistent connection to the RabbitMQ event bus broker.
    /// </summary>
    /// <remarks>
    ///     This broker is normally used in development environment.
    /// </remarks>
    public interface IRabbitMqPersistentConnection : IDisposable
    {
        /// <summary>
        ///     The state of validity of the connection.
        /// </summary>
        /// <returns>True if the connection is still in a state where it can be used, otherwise false.</returns>
        bool IsConnected { get; }

        /// <summary>
        ///     Try to acquire a persistent connection and subscribe to failure events.
        /// </summary>
        /// <returns>True if the connection is recovered, otherwise false.</returns>
        bool TryConnect();

        /// <summary>
        ///     Create and return a fresh channel, session, and model.
        /// </summary>
        /// <returns>The event bus channel.</returns>
        IModel CreateChannel();
    }
}