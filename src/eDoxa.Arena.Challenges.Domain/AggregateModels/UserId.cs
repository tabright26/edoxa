// Filename: UserId.cs
// Date Created: 2019-06-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.ComponentModel;

using eDoxa.Seedwork.Domain;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels
{
    [TypeConverter(typeof(EntityIdTypeConverter))]
    public sealed class UserId : EntityId<UserId>
    {
    }
}
