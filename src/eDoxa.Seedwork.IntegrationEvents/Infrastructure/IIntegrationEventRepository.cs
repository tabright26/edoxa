// Filename: IIntegrationEventRepository.cs
// Date Created: 2019-07-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Data.Common;
using System.Threading.Tasks;

namespace eDoxa.Seedwork.IntegrationEvents.Infrastructure
{
    public interface IIntegrationEventRepository
    {
        Task SaveIntegrationEventAsync(IntegrationEvent integrationEvent, DbTransaction transaction);

        Task MarkIntegrationEventAsPublishedAsync(IntegrationEvent integrationEvent);
    }
}
