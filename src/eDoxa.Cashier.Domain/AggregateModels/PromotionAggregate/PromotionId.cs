// Filename: RedeemCodeId.cs
// Date Created: 2020-01-17
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.ComponentModel;

using eDoxa.Seedwork.Domain;

namespace eDoxa.Cashier.Domain.AggregateModels.PromotionAggregate
{
    [TypeConverter(typeof(EntityIdTypeConverter))]
    public sealed class PromotionId : EntityId<PromotionId>
    {
    }
}
