// Filename: NotificationMetadata.cs
// Date Created: 2019-03-26
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

namespace eDoxa.Notifications.Domain.AggregateModels.NotificationAggregate
{
    public sealed class NotificationMetadata : HashSet<string>, INotificationMetadata
    {
    }
}