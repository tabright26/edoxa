// Filename: IIntegrationEventService.cs
// Date Created: 2019-04-08
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

namespace eDoxa.IntegrationEvents
{
    public interface IIntegrationEventService
    {
        Task PublishAsync(IntegrationEvent integrationEvent);
    }
}