﻿// Filename: INotificationMetadata.cs
// Date Created: 2019-03-26
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

namespace eDoxa.Notifications.Domain
{
    public interface INotificationMetadata : IReadOnlyCollection<string>
    {
    }
}