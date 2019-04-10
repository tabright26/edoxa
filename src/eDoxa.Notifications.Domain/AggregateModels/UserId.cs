﻿// Filename: UserId.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.ComponentModel;

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Notifications.Domain.AggregateModels
{
    [TypeConverter(typeof(EntityIdTypeConverter))]
    public sealed class UserId : EntityId<UserId>
    {
    }
}