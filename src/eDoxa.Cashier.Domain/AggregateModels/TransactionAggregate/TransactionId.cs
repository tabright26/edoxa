// Filename: TransactionId.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.ComponentModel;

using eDoxa.Seedwork.Domain;

namespace eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate
{
    [TypeConverter(typeof(EntityIdTypeConverter))]
    public sealed class TransactionId : EntityId<TransactionId>
    {
    }
}
