// Filename: TransactionId.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.ComponentModel;

namespace eDoxa.Seedwork.Domain.Miscs
{
    [TypeConverter(typeof(EntityIdTypeConverter))]
    public sealed class TransactionId : EntityId<TransactionId>
    {
    }
}
