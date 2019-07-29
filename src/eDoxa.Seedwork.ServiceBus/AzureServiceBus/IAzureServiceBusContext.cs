// Filename: IAzureServiceBusContext.cs
// Date Created: 2019-07-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using Microsoft.Azure.ServiceBus;

namespace eDoxa.Seedwork.ServiceBus.AzureServiceBus
{
    public interface IAzureServiceBusContext : IDisposable
    {
        ServiceBusConnectionStringBuilder ConnectionStringBuilder { get; }

        ITopicClient CreateTopicClient();
    }
}
