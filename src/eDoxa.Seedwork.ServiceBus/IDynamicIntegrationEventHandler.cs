// Filename: IDynamicIntegrationEventHandler.cs
// Date Created: 2019-07-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

namespace eDoxa.Seedwork.ServiceBus
{
    public interface IDynamicIntegrationEventHandler
    {
        Task HandleAsync(dynamic integrationEvent);
    }
}
