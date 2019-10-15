// Filename: AccountId.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.ComponentModel;

namespace eDoxa.Seedwork.Domain.Miscs
{
    [TypeConverter(typeof(EntityIdTypeConverter))]
    public sealed class AccountId : EntityId<AccountId>
    {
    }
}
