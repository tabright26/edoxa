// Filename: AccountId.cs
// Date Created: 2019-06-25
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.ComponentModel;

namespace eDoxa.Seedwork.Domain.Misc
{
    [TypeConverter(typeof(EntityIdTypeConverter))]
    public sealed class MemberId : EntityId<MemberId>
    {
    }
}
