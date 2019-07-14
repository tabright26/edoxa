// Filename: RoleId.cs
// Date Created: 2019-07-12
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.ComponentModel;

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Identity.Domain.AggregateModels.RoleAggregate
{
    [TypeConverter(typeof(EntityIdTypeConverter))]
    public sealed class RoleId : EntityId<RoleId>
    {
    }
}
