// Filename: UserStatus.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

namespace eDoxa.Identity.Domain.AggregateModels.UserAggregate
{
    public enum UserStatus
    {
        Unknown = 0,
        Offline = 1,
        Invisible = 2,
        Absent = 3,
        Online = 4
    }
}