// Filename: IIntegrationEventPublisher.cs
// Date Created: 2019-07-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

namespace eDoxa.Seedwork.IntegrationEvents
{
    public interface IIntegrationEventPublisher
    {
        Task PublishAsync(IntegrationEvent integrationEvent);
    }
}
