// Filename: IAzurePersistentConnection.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using Microsoft.Azure.ServiceBus;

namespace eDoxa.ServiceBus.Azure
{
    /// <summary>
    ///     Create a persistent connection to the AzureServiceBus event bus broker.
    /// </summary>
    /// <remarks>
    ///     This broker is normally used in production environment.
    /// </remarks>
    public interface IAzurePersistentConnection : IDisposable
    {
        /// <summary>
        ///     Used to generate Service Bus connection strings.
        /// </summary>
        /// <value>
        ///     The <see cref="T:Microsoft.Azure.ServiceBus.ServiceBusConnectionStringBuilder" />.
        /// </value>
        ServiceBusConnectionStringBuilder ConnectionStringBuilder { get; }

        /// <summary>
        ///     Create an instance of <see cref="T:Microsoft.Azure.ServiceBus.ITopicClient" />.
        /// </summary>
        /// <remarks>
        ///     <see cref="T:Microsoft.Azure.ServiceBus.ITopicClient" /> can be used for all basic interactions with a Service Bus
        ///     topic.
        /// </remarks>
        /// <returns>The <see cref="T:Microsoft.Azure.ServiceBus.ITopicClient" />.</returns>
        ITopicClient CreateTopicClient();
    }
}