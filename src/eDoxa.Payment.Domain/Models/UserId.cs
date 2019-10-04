// Filename: UserId.cs
// Date Created: 2019-10-04
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.ComponentModel;

using eDoxa.Seedwork.Domain;

namespace eDoxa.Payment.Domain.Models
{
    [TypeConverter(typeof(EntityIdTypeConverter))]
    public sealed class UserId : EntityId<UserId>
    {
    }
}
