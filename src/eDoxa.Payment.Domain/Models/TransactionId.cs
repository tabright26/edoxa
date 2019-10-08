// Filename: TransactionId.cs
// Date Created: 2019-10-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.ComponentModel;

using eDoxa.Seedwork.Domain;

namespace eDoxa.Payment.Domain.Models
{
    [TypeConverter(typeof(EntityIdTypeConverter))]
    public sealed class TransactionId : EntityId<TransactionId>
    {
    }
}
