// Filename: IntegrationEventStatus.cs
// Date Created: 2019-07-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

namespace eDoxa.Seedwork.IntegrationEvents.Infrastructure
{
    public enum IntegrationEventStatus
    {
        NotPublished = 0,
        Published = 1,
        PublishedFailed = 2
    }
}
