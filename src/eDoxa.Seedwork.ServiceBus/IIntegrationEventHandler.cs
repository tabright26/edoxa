// Filename: IIntegrationEventHandler.cs
// Date Created: 2019-07-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

namespace eDoxa.Seedwork.ServiceBus
{
    public interface IIntegrationEventHandler<in TIntegrationEvent> : IIntegrationEventHandler
    where TIntegrationEvent : IntegrationEvent
    {
        Task HandleAsync(TIntegrationEvent integrationEvent);
    }

    public interface IIntegrationEventHandler
    {
        // Marker interface
    }
}
